using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class LikePostDeletedConsumer : IConsumer<LikePostDeleted>
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<Comment> _commentRepository;

        public LikePostDeletedConsumer(IGenericRepository<Post> postRepository, IGenericRepository<Comment> commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<LikePostDeleted> context)
        {
            var post = await _postRepository.GetAsync(x => x.Id == context.Message.TargetId);

            if (post != null)
            {
                post.LikesCount--;
                if (post.LikesCount < 0)
                    post.LikesCount = 0;
                
                await _postRepository.UpdateAsync(post);
                return;
            }

            var comment = await _commentRepository.GetAsync(x => x.Id == context.Message.TargetId);

            if (comment != null)
            {
                comment.LikesCount--;
                await _commentRepository.UpdateAsync(comment);
            }
        }
    }
}