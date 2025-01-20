using SimpleAPI.Models;

namespace SimpleAPI.Services.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIDAsync(int userId);
        Task InsertUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task UpdateUserAsync(User user);
        Task SaveAsync();
    }
}
