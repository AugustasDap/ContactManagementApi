using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.Database.Models;

namespace ContactManagementApi.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string userName, string password);
        Task<User> SignupNewUser(string userName, string password);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(Guid id);
    }
}