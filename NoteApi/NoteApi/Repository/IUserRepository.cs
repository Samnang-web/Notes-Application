using NoteApi.Models;

namespace NoteApi.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<int> CreateAsync(User user);
    }
}
