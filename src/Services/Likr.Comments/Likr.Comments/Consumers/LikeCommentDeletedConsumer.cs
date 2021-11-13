using System.Threading.Tasks;
using Likr.Commands;
using Likr.Comments.Interfaces;
using MassTransit;

namespace Likr.Comments.Consumers
{
    public class LikeCommentDeletedConsumer : IConsumer<LikeCommentDeleted>
    {
        private readonly ICommentRepository _commentRepository;

        public LikeCommentDeletedConsumer(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<LikeCommentDeleted> context)
        {
            var comment = await _commentRepository.Get(context.Message.TargetId);

            if (comment != null)
            {
                comment.LikesCount--;
                
                if (comment.LikesCount < 0)
                    comment.LikesCount = 0;
                
                await _commentRepository.InsertOrUpdate(comment);
            }
        }
    }
}