using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.Database.Models;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        Task<Person> AddPersonAsync(PersonDto personDto, string userId);
        Task UpdatePersonAsync(Guid id, PersonDto personDto, string userId);
        Task<PersonDto> GetPersonByIdAsync(Guid id, string userId);
        Task<IEnumerable<Person>> GetAllPersonsAsync(string userId);
        Task DeletePersonAsync(Guid id, string userId);
        Task GetPersonByNameAsync(string name, string userId);
    }
}