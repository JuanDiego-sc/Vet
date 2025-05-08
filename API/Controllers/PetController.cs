using Application.Pets.Commands;
using Application.Pets.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class PetController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext context = context;

         public async Task<ActionResult<List<Pet>>> GetPetList()
        {
            return await Mediator.Send(new GetPetList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(string id)
        {
            return await Mediator.Send(new GetPet.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreatePet(Pet pet)
        {
            return await Mediator.Send(new CreatePet.Command { Pet = pet });
        }

        [HttpPut]
        public async Task<ActionResult> EditPet(Pet pet)
        {
        await Mediator.Send(new EditPet.Command { Pet = pet });
        return NoContent ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePet(string id)
        {
            await Mediator.Send(new DeletePet.Command { Id = id });
            return Ok();
        }
    }
}
