using System;
using System.Threading.Tasks;
using Likr.Commands;
using Likr.Comments.Entities;
using Likr.Comments.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Likr.Comments.Consumers
{
    public class CommentUserCreatedConsumer : IConsumer<CommentUserCreated>
    {
        private readonly IRavenDbStore _context;
        private readonly ILogger<CommentUserCreatedConsumer> _logger;

        public CommentUserCreatedConsumer(IRavenDbStore context, ILogger<CommentUserCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CommentUserCreated> context)
        {
            var message = context.Message;

            var user = new User
            {
                Id = message.Id,
                DisplayName = message.DisplayName,
                Username = message.Username
            };

            using var session = _context.Store.OpenAsyncSession();

            try
            {
                await session.StoreAsync(user, user.Id ?? Guid.NewGuid().ToString());
                await session.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to store comment in database");
            }
        }
    }
}