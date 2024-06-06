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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);            
            await _context.SaveChangesAsync();
        }

        
    }
}