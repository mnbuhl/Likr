using System.Threading.Tasks;
using Likr.Commands;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;

namespace Likr.Likes.Consumers
{
    public class CommentCreatedConsumer : IConsumer<CommentCreated>
    {
        private readonly IGenericRepository<Post> _commentRepository;

        public CommentCreatedConsumer(IGenericRepository<Post> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<CommentCreated> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment != null)
                return;

            comment = new Post
            {
                Id = message.Id,
                Body = message.Body,
                UserId = message.UserId,
            };
            
            await _commentRepository.CreateAsync(comment);
        }
    }
}