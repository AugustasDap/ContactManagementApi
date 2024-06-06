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
        public async Task<PersonDto> AddPerson([FromForm] PersonUpdateDto person)
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

        [HttpGet("GetPhotoByPersonId")]
        public async Task<ActionResult<PersonDto>> GetPhotoByPersonId(Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var filePath = await _personService.GetPersonPhotoPathAsync(id, userId);

                if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                {
                    return NotFound("Photo not found.");
                }

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                var contentType = "application/octet-stream";
                return File(memory, contentType, Path.GetFileName(filePath));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromForm] PersonUpdateDto personUpdateDto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var success = await _personService.UpdatePersonAsync(id, personUpdateDto, userId);
                if (success)
                {
                    return Ok("Updated successfully"); 
                }
                return BadRequest("Update failed."); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Person not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletePerson")]
        public async Task<IActionResult> DeletePerson (Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var isDeleted = await _personService.DeletePersonAsync(id, userId);
            if (!isDeleted)
            {
                return NotFound("Person not found");
            }
            return Ok("Person deleted");
        }
    }
}