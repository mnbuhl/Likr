using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class LikeCreatedConsumer : IConsumer<LikeCreated>
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;

        public LikeCreatedConsumer(IGenericRepository<Comment> commentRepository, IGenericRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<LikeCreated> context)
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