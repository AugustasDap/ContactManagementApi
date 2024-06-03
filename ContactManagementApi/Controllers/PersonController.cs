using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactManagementApi.Controllers
{

    [Route("Person/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("AddPerson")]  
        public async Task<Person> AddPerson(PersonDto person)
        {
           var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var createdPerson = await _personService.AddPersonAsync(person, userId);
            return createdPerson;
    
        }
        [HttpGet("GetPersonById")]
        public async Task<ActionResult<PersonDto>> GetPersonById(Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            try
            {
                var person = await _personService.GetPersonByIdAsync(id, userId);
                              

                return Ok(person);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}