using ContactManagementApi.Database.Models;


namespace ContactManagementApi.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
