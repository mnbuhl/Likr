using System.Threading.Tasks;
using Likr.Commands;
using Likr.Comments.Interfaces;
using MassTransit;

namespace Likr.Comments.Consumers
{
    public class LikeCommentCreatedConsumer : IConsumer<LikeCommentCreated>
    {
        private readonly ICommentRepository _commentRepository;

        public LikeCommentCreatedConsumer(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<LikeCommentCreated> context)
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