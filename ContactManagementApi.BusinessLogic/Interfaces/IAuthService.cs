using ContactManagementApi.Database.Models;

namespace ContactManagementApi.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string userName, string password);
        Task<User> SignupNewUser(string userName, string password);
    }
}