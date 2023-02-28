using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using Projekt001.Repo.Repositories;

namespace Projekt001.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonRepository repo;
        public PersonController(IPersonRepository temp)
        {
            repo = temp;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPerson()
        {
            List <Person> persons = await repo.getPersons();
            return Ok(persons);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            try
            {
                Person person = await repo.getPersonById(id);
                return Ok(person);
            }
            catch 
            {
                return BadRequest("Person not found");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetPersonsByName(string name)
        {
            try
            {
                List<Person> persons = await repo.getPersonsByName(name);
                return Ok(persons);
            }
            catch
            {
                return BadRequest("Person not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            try 
            { 
                await repo.createPerson(person);
                return CreatedAtAction("createPerson", person);
            }
            catch 
            {
                return BadRequest("Person not created");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            try 
            {
                await repo.updatePerson(person);
                return CreatedAtAction("updatePerson", person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                await repo.deletePerson(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
