using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class LikePostCreatedConsumer : IConsumer<LikePostCreated>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;

        public LikePostCreatedConsumer(IGenericRepository<Comment> commentRepository, IGenericRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<LikePostCreated> context)
        {
            var post = await _postRepository.GetAsync(x => x.Id == context.Message.TargetId);

            if (post != null)
            {
                post.LikesCount++;
                await _postRepository.UpdateAsync(post);
                return;
            }

            var comment = await _commentRepository.GetAsync(x => x.Id == context.Message.TargetId);

            if (comment != null)
            {
                comment.LikesCount++;
                await _commentRepository.UpdateAsync(comment);
            }
        }
    }
}