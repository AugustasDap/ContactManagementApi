using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace ContactManagementApi.Database.Repositories
{
    internal class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Person> AddPersonAsync(Person person)
        {
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> GetPersonByIdAsync(Guid id, string userId)
        {
            return await _context.People.Include(n => n.PlaceOfResidence)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == new Guid(userId));
        }

        
        public async Task UpdatePersonAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Person>> GetAllPersonsByUserIdAsync(Guid userId)
        {
            return await _context.People
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
