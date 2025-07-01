using NoteApi.DTOs;
using NoteApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NoteApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;

        public NoteController(INoteService service)
        {
            _service = service;
        }

        // Get current user ID from JWT claims
        private int? GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return null;

            return int.TryParse(userIdString, out var userId) ? userId : null;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            var notes = await _service.GetAllNotesByUser(userId.Value);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            var note = await _service.GetNoteById(id, userId.Value);
            return note == null ? NotFound() : Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            dto.UserId = userId.Value;

            var newNoteId = await _service.CreateNote(dto);

            return CreatedAtAction(nameof(Get), new { id = newNoteId }, new { Id = newNoteId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NoteUpdateDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            var result = await _service.UpdateNote(id, userId.Value, dto);
            if (!result)
                return NotFound();

            return Ok("Note updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not authenticated.");

            var result = await _service.DeleteNote(id, userId.Value);
            if (!result)
                return NotFound();

            return Ok("Note deleted");
        }
    }
}
