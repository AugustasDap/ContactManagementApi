using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
    }
}