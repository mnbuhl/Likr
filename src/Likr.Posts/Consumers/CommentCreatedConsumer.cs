using System;
using System.Threading.Tasks;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Comments.Commands
{
    public record CommentCreated(string Id, string Body, string UserId, Guid PostId);

    public class CommentCreatedConsumer : IConsumer<CommentCreated>
    {
        private readonly IGenericRepository<Comment> _repository;

        public CommentCreatedConsumer(IGenericRepository<Comment> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CommentCreated> context)
        {
            var message = context.Message;

            var comment = await _repository.GetAsync(x => x.Id == message.Id);

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

            await _repository.CreateAsync(comment);
        }
    }
}