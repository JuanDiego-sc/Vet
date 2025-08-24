using Application.DTOs;
using Application.Pets.Commands;
using Application.Pets.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class PetController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext context = context;

        [HttpGet]
        public async Task<ActionResult<List<PetDto>>> GetPetList()
        {
            return await Mediator.Send(new GetPetList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(string id)
        {
            return HandleResult(await Mediator.Send(new GetPet.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreatePet(PetDto pet)
        {
            return HandleResult(await Mediator.Send(new CreatePet.Command { PetDto = pet }));
        }

        [HttpPut]
        public async Task<ActionResult> EditPet(PetDto pet)
        {
            await Mediator.Send(new EditPet.Command { PetDto = pet });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePet(string id)
        {
            await Mediator.Send(new DeletePet.Command { Id = id });
            return Ok();
        }
    }
}
