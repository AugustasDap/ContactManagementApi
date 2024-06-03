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

        public Task<Person> GetPersonByNameAsync(string name, string userId)
        {
            throw new NotImplementedException();
            //    var getPersonByName = await _context.People.SingleOrDefault(n => n.Name == name && n.UserId.ToString() == userId);
            //    return getPersonByName;
        }
    }
}
