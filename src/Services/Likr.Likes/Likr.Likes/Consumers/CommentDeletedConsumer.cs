using System.Threading.Tasks;
using Likr.Commands;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;

namespace Likr.Likes.Consumers
{
    public class CommentDeletedConsumer : IConsumer<CommentDeleted>
    {
        private readonly IGenericRepository<Post> _commentRepository;

        public CommentDeletedConsumer(IGenericRepository<Post> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<CommentDeleted> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment == null)
                return;

            await _commentRepository.DeleteAsync(message.Id);
        }
    }
}