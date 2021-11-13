using System.Threading.Tasks;
using Likr.Commands;
using Likr.Comments.Interfaces;
using MassTransit;

namespace Likr.Comments.Consumers
{
    public class LikeDeletedConsumer : IConsumer<LikeCommentDeleted>
    {
        private readonly ICommentRepository _commentRepository;

        public LikeDeletedConsumer(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<LikeCommentDeleted> context)
        {
            var comment = await _commentRepository.Get(context.Message.TargetId);

            if (comment != null)
            {
                comment.LikesCount--;
                await _commentRepository.InsertOrUpdate(comment);
            }
        }
    }
}