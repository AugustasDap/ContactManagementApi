using ContactManagementApi.Database.Models;

namespace ContactManagementApi.Database.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> GetPersonByNameAsync(string name, string userId);
        Task<Person> GetPersonByIdAsync(Guid id, string userId);
    }
}