using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class CommentDeletedConsumer : IConsumer<CommentDeleted>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;

        public CommentDeletedConsumer(IGenericRepository<Comment> commentRepository,
            IGenericRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<CommentDeleted> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment == null)
                return;

            bool deleted = await _commentRepository.DeleteAsync(message.Id);

            if (!deleted)
                return;

            var post = await _postRepository.GetAsync(x => x.Id == comment.PostId);

            if (post != null)
            {
                post.CommentsCount--;
                await _postRepository.UpdateAsync(post);
            }
            else
            {
                var commentToUpdate = await _commentRepository.GetAsync(x => x.Id == comment.PostId);
                
                if (commentToUpdate == null)
                    return;

                commentToUpdate.CommentsCount--;
                await _commentRepository.UpdateAsync(commentToUpdate);
            }
        }
    }
}