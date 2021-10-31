using System.Threading.Tasks;
using Likr.Commands;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using MassTransit;

namespace Likr.Posts.Consumers
{
    public class PostUserCreatedConsumer : IConsumer<PostUserCreated>
    {
        private readonly IGenericRepository<User> _userRepository;

        public PostUserCreatedConsumer(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<PostUserCreated> context)
        {
            var message = context.Message;
            var user = await _userRepository.GetAsync(x => x.Id == message.Id);
            
            if (user != null)
                return;

            user = new User
            {
                Id = message.Id,
                DisplayName = message.DisplayName,
                Username = message.Username
            };

            await _userRepository.CreateAsync(user);
        }
    }
}