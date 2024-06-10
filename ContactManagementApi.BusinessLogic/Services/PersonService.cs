using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Models;
using ContactManagementApi.Database.Repositories;
using Microsoft.AspNetCore.Http;


namespace ContactManagementApi.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly string _fileUploadPath = "C:\\Users\\Augustas\\source\\repos\\ContactManagementApi\\ContactManagementApi.Database\\Files\\";



        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            
            
        }

        public async Task<PersonDto> AddPersonAsync(PersonUpdateDto personUpdateDto, string userId) 
        {

            string filePath = null;
            if (personUpdateDto.File != null && personUpdateDto.File.Length > 0)
            {
                filePath = Path.Combine(_fileUploadPath, Guid.NewGuid().ToString() + Path.GetExtension(personUpdateDto.File.FileName));
                await FileImageSizeHelper.SaveImageAsync(filePath, personUpdateDto.File);
            }

            var person = new Person
            {
                
                Name = personUpdateDto.Name,
                LastName = personUpdateDto.LastName,
                Gender = personUpdateDto.Gender,
                Birthday = personUpdateDto.Birthday,
                PersonIdentificationCode = personUpdateDto.PersonIdentificationCode,
                PhoneNumber = personUpdateDto.PhoneNumber,
                Email = personUpdateDto.Email,
                FilePath = filePath,                
                PlaceOfResidence = new PlaceOfResidence
                    {
                        City = personUpdateDto.PlaceOfResidence.City,
                        Street = personUpdateDto.PlaceOfResidence.Street,
                        HouseNumber = personUpdateDto.PlaceOfResidence.HouseNumber,
                        ApartmentNumber = personUpdateDto.PlaceOfResidence.ApartmentNumber
                    },
                    UserId = new Guid(userId),
                };
                var createdPerson = await _personRepository.AddPersonAsync(person);
            var createdPersonDto = new PersonDto
            {
                Id = createdPerson.Id,
                Name = createdPerson.Name,
                LastName = createdPerson.LastName,
                Gender = createdPerson.Gender,
                Birthday = createdPerson.Birthday,
                PersonIdentificationCode = createdPerson.PersonIdentificationCode,
                PhoneNumber = createdPerson.PhoneNumber,
                Email = createdPerson.Email,
                PlaceOfResidence = new PlaceOfResidenceDto
                {
                    City = createdPerson.PlaceOfResidence.City,
                    Street = createdPerson.PlaceOfResidence.Street,
                    HouseNumber = createdPerson.PlaceOfResidence.HouseNumber,
                    ApartmentNumber = createdPerson.PlaceOfResidence.ApartmentNumber
                }
            };
            return createdPersonDto;

        }

        public async Task<bool> DeletePersonAsync(Guid id, string userId)
        {
            var person = await _personRepository.GetPersonByIdAsync(id, userId);
            if (person == null)
            {
                return false;
            }
            await _personRepository.DeletePersonAsync(person);
            return true;
        }



        public async Task<PersonDto> GetPersonByIdAsync(Guid id, string userId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            }

            var person = await _personRepository.GetPersonByIdAsync(id, userId);

            if (person == null)
            {
                throw new KeyNotFoundException("Person not found.");
            }
            // Convert Person entity to PersonDto
            var personDto = new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                Gender = person.Gender,
                Birthday = person.Birthday,
                PersonIdentificationCode = person.PersonIdentificationCode,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                //FilePath = person.FilePath,
                PlaceOfResidence = new PlaceOfResidenceDto
                {
                    City = person.PlaceOfResidence.City,
                    Street = person.PlaceOfResidence.Street,
                    HouseNumber = person.PlaceOfResidence.HouseNumber,
                    ApartmentNumber = person.PlaceOfResidence.ApartmentNumber
                }
            };
            return personDto;
        }

        public async Task<bool> UpdatePersonAsync(Guid id, PersonUpdateDto personUpdateDto, string userId)
        {
          
            var person = await _personRepository.GetPersonByIdAsync(id, userId);

            if (person == null)
            {
                throw new KeyNotFoundException("Person not found.");
            }

            // updeitinu Person
            person.Name = personUpdateDto.Name;
            person.LastName = personUpdateDto.LastName;
            person.Gender = personUpdateDto.Gender;
            person.Birthday = personUpdateDto.Birthday;
            person.PersonIdentificationCode = personUpdateDto.PersonIdentificationCode;
            person.PhoneNumber = personUpdateDto.PhoneNumber;
            person.Email = personUpdateDto.Email;
            //person.FilePath = personUpdateDto.FilePath;

            
            if (personUpdateDto.PlaceOfResidence != null)
            {
                if (person.PlaceOfResidence == null)
                {
                    person.PlaceOfResidence = new PlaceOfResidence();
                }
                person.PlaceOfResidence.City = personUpdateDto.PlaceOfResidence.City;
                person.PlaceOfResidence.Street = personUpdateDto.PlaceOfResidence.Street;
                person.PlaceOfResidence.HouseNumber = personUpdateDto.PlaceOfResidence.HouseNumber;
                person.PlaceOfResidence.ApartmentNumber = personUpdateDto.PlaceOfResidence.ApartmentNumber;
            }
            await _personRepository.UpdatePersonAsync(person); 
            return true;
        }
        public async Task<string> GetPersonPhotoPathAsync(Guid id, string userId)
        {
            var person = await _personRepository.GetPersonByIdAsync(id, userId);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found.");
            }

            return person.FilePath;
        }
        public async Task<IEnumerable<PersonMinimalDto>> GetAllPersonsByUserIdAsync(Guid userId)
        {
            var persons = await _personRepository.GetAllPersonsByUserIdAsync(userId);
            return persons.Select(person => new PersonMinimalDto
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName
            });
        }
        public async Task<IEnumerable<PersonMinimalDto>> GetAllPersonsForLoggedUserAsync(string userId)
        {
            var guidUserId = new Guid(userId);
            return await GetAllPersonsByUserIdAsync(guidUserId);
        }


    }
}