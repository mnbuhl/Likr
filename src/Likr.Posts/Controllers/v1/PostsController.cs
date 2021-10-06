using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Likr.Posts.Data;
using Likr.Posts.Dtos.v1;
using Likr.Posts.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            List<Post> posts = await _context.Posts.ToListAsync();

            var postDtos = posts.Select(x => new PostDto(x.Id, x.Body, x.UserId, null));

            return Ok(postDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(string id)
        {
            var post = await _context.Posts.FindAsync(id);

            return Ok(new PostDto(post.Id, post.Body, post.UserId, null));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto postDto)
        {
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Body = postDto.Body,
                UserId = postDto.UserId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = post.Id }, new PostDto(post.Id, post.Body, post.UserId, null));
        }
    }
}