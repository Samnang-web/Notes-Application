using NoteApi.DTOs;
using NoteApi.Models;

namespace NoteApi.Service
{
    public interface INoteService
    {

        Task<IEnumerable<Notes>> GetAllNotesByUser(int userId, string? searchQuery = null, string sortOption = "latest");
        Task<Notes?> GetNoteById(int id, int userId);
        Task<int> CreateNote(NoteCreateDto noteDto);
        Task<bool> UpdateNote(int id, int userId, NoteUpdateDto noteDto);
        Task<bool> DeleteNote(int id, int userId);
    }
}
