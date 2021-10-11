using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class CommentCreatedConsumer : IConsumer<CommentCreated>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;

        public CommentCreatedConsumer(IGenericRepository<Comment> commentRepository, IGenericRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<CommentCreated> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment != null)
                return;

            comment = new Comment
            {
                Id = message.Id,
                Body = message.Body,
                PostId = message.PostId,
                UserId = message.UserId,
                LikesCount = 0
            };
            
            bool created = await _commentRepository.CreateAsync(comment);
            
            if (!created)
                return;

            var post = await _postRepository.GetAsync(x => x.Id == comment.PostId);

            if (post != null)
            {
                post.CommentsCount++;

                await _postRepository.UpdateAsync(post);
            }
            else
            {
                var commentToUpdate = await _commentRepository.GetAsync(x => x.Id == comment.PostId);
                
                if (commentToUpdate == null)
                    return;

                commentToUpdate.CommentsCount++;
                await _commentRepository.UpdateAsync(commentToUpdate);
            }
        }
    }
}