using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Likr.Posts.Consumers
{
    public class CommentDeletedConsumer : IConsumer<CommentDeleted>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly ILogger<CommentDeletedConsumer> _logger;

        public CommentDeletedConsumer(IGenericRepository<Comment> commentRepository,
            IGenericRepository<Post> postRepository, ILogger<CommentDeletedConsumer> logger)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CommentDeleted> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment != null)
            {
                bool deleted = await _commentRepository.DeleteAsync(message.Id);

                if (!deleted)
                    return;
            }

            var commentToComment = await _commentRepository.GetAsync(x => x.Id == message.PostId);

            if (commentToComment == null)
            {
                _logger.LogInformation($"Tried to decrease CommentsCount of Comment with {message.PostId}, but no Comment found");
                return;
            }
            
            commentToComment.CommentsCount--;
            await _commentRepository.UpdateAsync(commentToComment);
        }
    }
}