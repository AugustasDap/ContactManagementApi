using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Models;
using ContactManagementApi.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementApi.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ApplicationDbContext _context;

        public PersonService(IPersonRepository personRepository, ApplicationDbContext context)
        {
            _personRepository = personRepository;
            _context = context;
        }

        public async Task<PersonDto> AddPersonAsync(PersonDto personDto, string userId)
        {
            //var existingPerson = _context.People
            //     .FirstOrDefaultAsync(a => a.PersonIdentificationCode == personDto.PersonIdentificationCode);

            //if (existingPerson != null)
            //{
            //    throw new Exception("Person with the given identification code already exists.");
            //}
            var person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = personDto.Name,
                    LastName = personDto.LastName,
                    Gender = personDto.Gender,
                    Birthday = personDto.Birthday,
                    PersonIdentificationCode = personDto.PersonIdentificationCode,
                    PhoneNumber = personDto.PhoneNumber,
                    Email = personDto.Email,
                    FilePath = personDto.FilePath,
                    PlaceOfResidence = new PlaceOfResidence
                    {
                        City = personDto.PlaceOfResidence.City,
                        Street = personDto.PlaceOfResidence.Street,
                        HouseNumber = personDto.PlaceOfResidence.HouseNumber,
                        ApartmentNumber = personDto.PlaceOfResidence.ApartmentNumber
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
                FilePath = createdPerson.FilePath,
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

        public Task DeletePersonAsync(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAllPersonsAsync(string userId)
        {
            throw new NotImplementedException();
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
                FilePath = person.FilePath,
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

        public Task UpdatePersonAsync(Guid id, PersonDto personDto, string userId)
        {
            throw new NotImplementedException();
        }

        public Task GetPersonByNameAsync(string name, string userId)
        {
            throw new NotSupportedException();
        }
    }
}