using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.Database.Models;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> AddPersonAsync(PersonUpdateDto personUpdateDto, string userId);
        Task<bool> UpdatePersonAsync(Guid id, PersonUpdateDto personUpdateDto, string userId);
        Task<PersonDto> GetPersonByIdAsync(Guid id, string userId);

        Task<bool> DeletePersonAsync(Guid id, string userId);
        Task<string> GetPersonPhotoPathAsync(Guid id, string userId);


    }
}