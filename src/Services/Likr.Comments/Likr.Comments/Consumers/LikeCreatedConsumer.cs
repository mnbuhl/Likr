using System.Threading.Tasks;
using Likr.Commands;
using Likr.Comments.Interfaces;
using MassTransit;

namespace Likr.Comments.Consumers
{
    public class LikeCreatedConsumer : IConsumer<LikeCreated>
    {
        private readonly ICommentRepository _commentRepository;

        public LikeCreatedConsumer(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<LikeCreated> context)
        {
            var comment = await _commentRepository.Get(context.Message.TargetId);

            if (comment != null)
            {
                comment.LikesCount++;
                await _commentRepository.InsertOrUpdate(comment);
            }
        }
    }
}