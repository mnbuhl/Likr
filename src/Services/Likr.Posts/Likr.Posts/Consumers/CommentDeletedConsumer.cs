using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class CommentDeletedConsumer: IConsumer<CommentDeleted>
    {
        private readonly IGenericRepository<Comment> _repository;

        public CommentDeletedConsumer(IGenericRepository<Comment> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CommentDeleted> context)
        {
            var message = context.Message;

            var comment = await _repository.GetAsync(x => x.Id == message.Id);
            
            if (comment == null)
                return;

            await _repository.DeleteAsync(message.Id);
        }
    }
}