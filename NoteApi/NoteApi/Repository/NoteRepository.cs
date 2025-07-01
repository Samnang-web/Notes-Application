using NoteApi.Models;
using Dapper;
using System.Data;

namespace NoteApi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDbConnection _conn;

        public NoteRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Notes>> GetAllNotesByUser(int userId)
        {
            var sql = "SELECT * FROM Notes WHERE UserId = @UserId ORDER BY CreatedAt DESC";
            var notes = await _conn.QueryAsync<Notes>(sql, new { UserId = userId });
            return notes.ToList();
        }

        public async Task<Notes?> GetNoteById(int id, int userId)
        {
            var note = await _conn.QuerySingleOrDefaultAsync<Notes>(
                "SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId",
                new { Id = id, UserId = userId });

            return note;
        }

        public async Task<int> CreateNote(Notes note)
        {
            var sql = @"INSERT INTO Notes (Title, Content, CreatedAt, UserId) VALUES (@Title, @Content, @CreatedAt, @UserId) RETURNING Id;";

            var insertedId = await _conn.ExecuteScalarAsync<int>(sql, note);
            return insertedId;
        }

        public async Task<int> UpdateNote(Notes note)
        {
            var sql = @"UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE Id = @Id AND UserId = @UserId";

            var rowsAffected = await _conn.ExecuteAsync(sql, note);
            return rowsAffected;
        }

        public async Task<int> DeleteNote(int id, int userId)
        {
            var sql = "DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId";
            var rowsAffected = await _conn.ExecuteAsync(sql, new { Id = id, UserId = userId });
            return rowsAffected;
        }
    }
}
