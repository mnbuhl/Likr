using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class CommentCreatedConsumer : IConsumer<CommentCreated>
    {
        private readonly IGenericRepository<Comment> _commentRepository;

        public CommentCreatedConsumer(IGenericRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task Consume(ConsumeContext<CommentCreated> context)
        {
            var message = context.Message;

            var comment = await _commentRepository.GetAsync(x => x.Id == message.Id);

            if (comment != null)
                return;

            comment = new Comment
            {
                Id = message.Id,
                Body = message.Body,
                PostId = message.PostId,
                UserId = message.UserId,
                LikesCount = 0
            };
            
            await _commentRepository.CreateAsync(comment);
        }
    }
}