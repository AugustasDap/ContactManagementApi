using ContactManagementApi.Database.Models;

namespace ContactManagementApi.Database.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task DeleteUserAsync(User user);
    }
}