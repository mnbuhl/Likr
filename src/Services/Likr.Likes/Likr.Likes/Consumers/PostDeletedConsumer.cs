using System.Threading.Tasks;
using Likr.Commands;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;

namespace Likr.Likes.Consumers
{
    public class PostDeletedConsumer : IConsumer<PostDeleted>
    {
        private readonly IGenericRepository<Post> _postRepository;

        public PostDeletedConsumer(IGenericRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<PostDeleted> context)
        {
            var message = context.Message;

            var comment = await _postRepository.GetAsync(x => x.Id == message.Id);

            if (comment == null)
                return;

            await _postRepository.DeleteAsync(message.Id);
        }
    }
}