using Application.AppointmentDetails.Queries;
using Application.Diseases.Commands;
using Application.Diseases.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class DiseaseController(AppDbContext context) :BaseApiController
    {
        public readonly AppDbContext context = context;

        [HttpGet]	
        //Good practice to use async/await for database queries
        public async Task<ActionResult<List<Disease>>> GetDiseases()
        {
            return await Mediator.Send(new GetDiseaseList.Query());
        }


        [HttpGet("{id}")]	
        public async Task<ActionResult<Disease>> GetDisease(string id)
        {
            return await Mediator.Send(new GetDisease.Query {Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateDisease(Disease disease)
        {
            return await Mediator.Send(new CreateDisease.Command { Disease = disease });
        }

        [HttpPut]
        public async Task<ActionResult> EditDisease(Disease disease)
        {
        await Mediator.Send(new EditDisease.Command { Disease = disease });
        return NoContent ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDisease(string id)
        {
            await Mediator.Send(new DeleteDisease.Command { Id = id });
            return Ok();
        }
    }

}
