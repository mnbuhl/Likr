using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Likr.Commands;
using Likr.Likes.Dtos.v1;
using Likr.Likes.Entities;
using Likr.Likes.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Likr.Likes.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IGenericRepository<Like> _likeRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public LikesController(IGenericRepository<Like> likeRepository, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [Authorize]
        [HttpPost("like")]
        public async Task<ActionResult<LikeDto>> Like(CreateLikeDto likeDto)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || likeDto.ObserverId != Guid.Parse(userId))
                return Unauthorized();
            
            var like = await _likeRepository.GetAsync(x =>
                x.ObserverId == userId && x.TargetId == likeDto.TargetId.ToString());

            if (like != null)
                return BadRequest("You already liked this post");

            like = new Like
            {
                ObserverId = userId,
                TargetId = likeDto.TargetId.ToString()
            };

            bool created = await _likeRepository.CreateAsync(like);

            if (!created)
                return BadRequest();

            var createdLikeDto = _mapper.Map<LikeDto>(like);

            await _publishEndpoint.Publish(new LikePostCreated(like.TargetId));
            await _publishEndpoint.Publish(new LikeCommentCreated(like.TargetId));

            return Ok(createdLikeDto);
        }

        [Authorize]
        [HttpDelete("unlike/{postId}")]
        public async Task<IActionResult> Unlike(string postId)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            
            var like = await _likeRepository.GetAsync(x =>
                x.ObserverId == userId && x.TargetId == postId);

            if (like == null)
                return BadRequest("You haven't liked this post");

            bool deleted = await _likeRepository.DeleteAsync(like);

            if (!deleted)
                return BadRequest();

            await _publishEndpoint.Publish(new LikePostDeleted(postId));
            await _publishEndpoint.Publish(new LikeCommentDeleted(postId));

            return NoContent();
        }
    }
}