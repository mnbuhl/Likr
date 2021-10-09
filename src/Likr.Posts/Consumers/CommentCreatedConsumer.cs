using System.Threading.Tasks;
using Likr.Comments.Dtos.v1;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class CommentCreatedConsumer : IConsumer<CommentCreated>
    {
        public Task Consume(ConsumeContext<CommentCreated> context)
        {
            throw new System.NotImplementedException();
        }
    }
}