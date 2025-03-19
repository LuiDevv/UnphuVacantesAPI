using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using api.Interfaces;
using api.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
        }

        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            
            return Ok(_mapper.Map<CommentDTO>(comment));
        }

        // POST: api/comments

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment(CreateCommentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
                return BadRequest("El contenido no puede estar vacío.");

            var comment = _mapper.Map<Comment>(request);
            comment.Id = Guid.NewGuid();
            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            var commentDTO = _mapper.Map<CommentDTO>(comment);

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDTO);
        }


        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, UpdateCommentRequest request)
        {
            var existingComment = await _commentRepository.GetByIdAsync(id);
            if (existingComment == null)
                return NotFound();
            
            if (string.IsNullOrWhiteSpace(request.Content))
                return BadRequest("El contenido no puede estar vacío.");

            _mapper.Map(request, existingComment);
            await _commentRepository.UpdateAsync(existingComment);

            return NoContent();
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            await _commentRepository.DeleteAsync(id);
            return NoContent();
        }
    }

    
}
