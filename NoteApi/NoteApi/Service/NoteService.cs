using NoteApi.DTOs;
using NoteApi.Models;
using NoteApi.Repository;

namespace NoteApi.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Notes>> GetAllNotesByUser(
            int userId, string? searchQuery = null, string sortOption = "latest")
        {
            return await _noteRepository.GetAllNotesByUser(userId);
        }

        public async Task<Notes?> GetNoteById(int id, int userId)
        {
            return await _noteRepository.GetNoteById(id, userId);
        }

        public async Task<int> CreateNote(NoteCreateDto noteDto)
        {
            // Manually map NoteCreateDto to Notes model
            var note = new Notes
            {
                Title = noteDto.Title,
                Content = noteDto.Content,
                UserId = noteDto.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            return await _noteRepository.CreateNote(note);
        }

        public async Task<bool> UpdateNote(int id, int userId, NoteUpdateDto noteDto)
        {
            var existingNote = await _noteRepository.GetNoteById(id, userId);
            if (existingNote == null)
                return false;

            existingNote.Title = noteDto.Title;
            existingNote.Content = noteDto.Content;
            existingNote.UpdatedAt = DateTime.UtcNow;

            var rows = await _noteRepository.UpdateNote(existingNote);
            return rows > 0;
        }

        public async Task<bool> DeleteNote(int id, int userId)
        {
            var existingNote = await _noteRepository.GetNoteById(id, userId);
            if (existingNote == null)
                return false;

            var rows = await _noteRepository.DeleteNote(id, userId);
            return rows > 0;
        }
    }
}
