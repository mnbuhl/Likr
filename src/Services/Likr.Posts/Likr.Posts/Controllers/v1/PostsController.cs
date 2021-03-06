using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Likr.Commands;
using Likr.Posts.Dtos.v1;
using Likr.Posts.Entities;
using Likr.Posts.Helpers;
using Likr.Posts.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public PostsController(IGenericRepository<Post> postRepository, IMapper mapper,
            IGenericRepository<Comment> commentRepository, IPublishEndpoint publishEndpoint)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IList<PostDto>>> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            IList<Post> posts = await _postRepository.GetAllAsync(paginationQuery: paginationQuery, orderBy: x => x.OrderByDescending(p => p.CreatedAt),
                includes: x => x.Include(p => p.User));

            return Ok(_mapper.Map<IList<PostDto>>(posts));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IList<PostDto>>> GetAllByUserId([FromRoute] string userId,
            [FromQuery] PaginationQuery paginationQuery)

        {
            IList<Post> posts =
                await _postRepository.GetAllAsync(x => x.UserId == userId, paginationQuery: paginationQuery,
                    includes: x => x.Include(p => p.User), orderBy: x => x.OrderByDescending(p => p.CreatedAt));

            if (posts == null)
                return NotFound();

            return Ok(_mapper.Map<IList<PostDto>>(posts));
        }
        
        [HttpGet("username/{username}")]
        public async Task<ActionResult<IList<PostDto>>> GetAllByUsername([FromRoute] string username,
            [FromQuery] PaginationQuery paginationQuery)

        {
            IList<Post> posts =
                await _postRepository.GetAllAsync(x => x.User.Username == username, paginationQuery: paginationQuery,
                    includes: x => x.Include(p => p.User), orderBy: x => x.OrderByDescending(p => p.CreatedAt));

            if (posts == null)
                return NotFound();

            return Ok(_mapper.Map<IList<PostDto>>(posts));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDto>> Get(Guid id)
        {
            var post = await _postRepository.GetAsync(x => x.Id == id.ToString(),
                includes: x => x.Include(p => p.User));

            if (post == null)
                return NotFound();

            post.Comments =
                await _commentRepository.GetAllAsync(x => x.PostId == id.ToString(), x => x.Include(p => p.User));

            return Ok(_mapper.Map<PostDto>(post));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);

            bool created = await _postRepository.CreateAsync(post);

            if (!created)
                return BadRequest(ModelState);

            await _publishEndpoint.Publish(new PostCreated(post.Id, post.Body, post.UserId));

            return CreatedAtAction("Get", new { id = post.Id }, _mapper.Map<PostDto>(post));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _postRepository.GetAsync(x => x.Id == id.ToString(), x => x.Include(p => p.User));
            
            if (post == null)
                return NotFound();

            if (post.User.Id != userId)
                return BadRequest();
            
            bool deleted = await _postRepository.DeleteAsync(id.ToString());

            if (!deleted)
                return NotFound();

            await _publishEndpoint.Publish(new PostDeleted(id.ToString()));

            return NoContent();
        }
    }
}