using Application.AppointmentDetails.Queries;
using Application.Diseases.Commands;
using Application.Diseases.Queries;
using Application.DTOs;
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
        public async Task<ActionResult<List<DiseaseDto>>> GetDiseases()
        {
            return await Mediator.Send(new GetDiseaseList.Query());
        }


        [HttpGet("{id}")]	
        public async Task<ActionResult<DiseaseDto>> GetDisease(string id)
        {
            return HandleResult(await Mediator.Send(new GetDisease.Query {Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateDisease(DiseaseDto disease)
        {
            return HandleResult(await Mediator.Send(new CreateDisease.Command { DiseaseDto = disease }));
        }

        [HttpPut]
        public async Task<ActionResult> EditDisease(DiseaseDto disease)
        {
        await Mediator.Send(new EditDisease.Command { DiseaseDto = disease });
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
