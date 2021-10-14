using System.Threading.Tasks;
using Likr.Commands;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;

namespace Likr.Likes.Consumers
{
    public class PostCreatedConsumer : IConsumer<PostCreated>
    {
        private readonly IGenericRepository<Post> _postRepository;

        public PostCreatedConsumer(IGenericRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<PostCreated> context)
        {
            var message = context.Message;

            var comment = await _postRepository.GetAsync(x => x.Id == message.Id);

            if (comment != null)
                return;

            comment = new Post
            {
                Id = message.Id,
                Body = message.Body,
                UserId = message.UserId,
            };
            
            await _postRepository.CreateAsync(comment);
        }
    }
}