using NoteApi.Models;
using Dapper;
using System.Data;

namespace NoteApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _conn;
        public UserRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            var user = await _conn.QuerySingleOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @Email", new { Email = email });
            return user;
        }

        public async Task<int> CreateAsync(User user)
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            var rows = await _conn.ExecuteAsync(
                "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)", user);
            return rows;
        }
    }
}
