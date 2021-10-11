using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Likr.Likes.Dtos.v1;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Likr.Likes.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IGenericRepository<Like> _likeRepository;
        private readonly IMapper _mapper;

        public LikesController(IGenericRepository<Like> likeRepository, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

        [HttpGet("posts/{postId:guid}")]
        public async Task<ActionResult<IList<LikeDto>>> GetLikesByPostId(Guid postId)
        {
            var likes = await _likeRepository.GetAllAsync(x => x.TargetId == postId.ToString(), 
                x => x.Include(l => l.Target).Include(l => l.Observer));

            return Ok(_mapper.Map<IList<LikeDto>>(likes));
        }
        
        [HttpGet("users/{userId:guid}")]
        public async Task<ActionResult<IList<LikeDto>>> GetLikesByUserId(Guid userId)
        {
            var likes = await _likeRepository.GetAllAsync(x => x.ObserverId == userId.ToString(),
                x => x.Include(l => l.Target).Include(l => l.Observer));

            return Ok(_mapper.Map<IList<LikeDto>>(likes));
        }

        [HttpPost]
        public async Task<ActionResult> Like(CreateLikeDto likeDto)
        {
            var like = _mapper.Map<Like>(likeDto);

            bool created = await _likeRepository.CreateAsync(like);

            if (!created)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Unlike(Guid postId)
        {
            bool deleted = await _likeRepository.DeleteAsync(postId.ToString());

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}