using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Likr.Posts.Dtos.v1;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        //private readonly IPostRepository _repository;
        private readonly IGenericRepository<Post> _repository;
        private readonly IMapper _mapper;

        public PostsController(IGenericRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<PostDto>>> GetAll()
        {
            IList<Post> posts = await _repository.GetAllAsync();

            return Ok(_mapper.Map<IList<PostDto>>(posts));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IList<PostDto>>> GetAllByUserId(string userId)
        
        {
            IList<Post> posts = await _repository.GetAllAsync(x => x.UserId == userId);

            if (posts == null)
                return NotFound();

            return Ok(_mapper.Map<IList<PostDto>>(posts));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDto>> Get(Guid id)
        {
            var post = await _repository.GetAsync(x => x.Id == id.ToString(), x => x.Include(p => p.Comments));

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);

            bool created = await _repository.CreateAsync(post);

            if (!created)
                return BadRequest(ModelState);

            return CreatedAtAction("Get", new { id = post.Id }, _mapper.Map<PostDto>(post));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await _repository.DeleteAsync(id.ToString());

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}