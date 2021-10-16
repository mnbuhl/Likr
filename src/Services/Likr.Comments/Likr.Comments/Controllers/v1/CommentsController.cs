using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Likr.Commands;
using Likr.Comments.Dtos.v1;
using Likr.Comments.Entities;
using Likr.Comments.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Likr.Comments.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CommentsController(ICommentRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CommentDto>>> GetAll()
        {
            IList<Comment> comments = await _repository.GetAll();

            return Ok(_mapper.Map<IList<CommentDto>>(comments));
        }

        [HttpGet("post/{postId:guid}")]
        public async Task<ActionResult<IList<CommentDto>>> GetCommentsByPostId(Guid postId)
        {
            IList<Comment> comments = await _repository.GetAllByPostId(postId.ToString());

            if (comments == null)
                return NotFound();

            return Ok(_mapper.Map<IList<CommentDto>>(comments));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CommentDto>> Get(Guid id)
        {
            var comment = await _repository.Get(id.ToString());

            if (comment == null)
                return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            bool created = await _repository.InsertOrUpdate(comment);

            if (!created)
                return BadRequest();

            await _publishEndpoint.Publish(new CommentCreated(comment.Id, comment.Body, comment.UserId,
                comment.PostId));

            await _publishEndpoint.Publish(new PostCreated(comment.Id, comment.Body, comment.UserId));

            return CreatedAtAction("Get", new { id = comment.Id }, _mapper.Map<CommentDto>(comment));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await _repository.Get(id.ToString());

            if (comment == null)
                return NotFound();

            await _repository.Delete(Guid.Parse(comment.Id));

            await _publishEndpoint.Publish(new CommentDeleted(id.ToString(), comment.PostId));
            await _publishEndpoint.Publish(new PostDeleted(id.ToString()));

            return NoContent();
        }
    }
}