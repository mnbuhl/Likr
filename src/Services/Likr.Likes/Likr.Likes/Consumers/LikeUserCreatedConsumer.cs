using System.Threading.Tasks;
using Likr.Commands;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;

namespace Likr.Likes.Consumers
{
    public class LikeUserCreatedConsumer : IConsumer<LikeUserCreated>
    {
        private readonly IGenericRepository<User> _userRepository;

        public LikeUserCreatedConsumer(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<LikeUserCreated> context)
        {
            var message = context.Message;
            var user = await _userRepository.GetAsync(x => x.Id == message.Id);
            
            if (user != null)
                return;

            user = new User
            {
                Id = message.Id,
                DisplayName = message.DisplayName,
                UserName = message.Username
            };

            await _userRepository.CreateAsync(user);
        }
    }
}