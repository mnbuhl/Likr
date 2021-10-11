using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Likr.Likes.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IGenericRepository<Like> _likeRepository;

        public LikesController(IGenericRepository<Like> likeRepository)
        {
            _likeRepository = likeRepository;
        }
    }
}