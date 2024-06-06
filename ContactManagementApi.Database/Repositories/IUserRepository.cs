using ContactManagementApi.Database.Models;

namespace ContactManagementApi.Database.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task DeleteUserAsync(User user);
    }
}