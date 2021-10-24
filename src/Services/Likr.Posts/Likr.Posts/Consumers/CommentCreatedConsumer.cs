using System.Threading.Tasks;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Likr.Posts.Consumers
{
    public class CommentCreatedConsumer : IConsumer<Likr.Commands.CommentCreated>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly ILogger<CommentCreatedConsumer> _logger;

        public CommentCreatedConsumer(IGenericRepository<Comment> commentRepository,
            IGenericRepository<Post> postRepository, ILogger<CommentCreatedConsumer> logger)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Likr.Commands.CommentCreated> context)
        {
            var message = context.Message;

            bool commentToPost = (await _postRepository.GetAsync(x => x.Id == message.PostId)) != null;
            _logger.LogInformation($"commentToPost: {commentToPost}");

            if (commentToPost)
            {
                bool exists = (await _commentRepository.GetAsync(x => x.Id == message.Id)) != null;
                _logger.LogInformation($"exists: {exists}");

                if (exists)
                    return;

                await _commentRepository.CreateAsync(new Comment
                {
                    Id = message.Id,
                    Body = message.Body,
                    PostId = message.PostId,
                    UserId = message.UserId,
                    LikesCount = 0,
                    CommentsCount = 0
                });
                return;
            }

            // Comment to comment logic
            var comment = await _commentRepository.GetAsync(x => x.Id == message.PostId);

            if (comment == null)
            {
                _logger.LogInformation(
                    $"Tried to increase CommentsCount on Comment but no Comment was with Id {message.Id} was found");
                return;
            }

            comment.CommentsCount++;
            await _commentRepository.UpdateAsync(comment);
        }
    }
}