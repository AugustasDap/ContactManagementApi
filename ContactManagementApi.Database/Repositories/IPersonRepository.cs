using ContactManagementApi.Database.Models;

namespace ContactManagementApi.Database.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(Guid id, string userId);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task<IEnumerable<Person>> GetAllPersonsByUserIdAsync(Guid userId);
    }
}