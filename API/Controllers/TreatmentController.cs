using Application.Treatments.Commands;
using Application.Treatments.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class TreatmentController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext context = context;

         public async Task<ActionResult<List<Treatment>>> GetTreatmentList()
        {
            return await Mediator.Send(new GetTreatmentList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Treatment>> GetTreatment(string id)
        {
            return await Mediator.Send(new GetTreatment.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateTreatment(Treatment treatment)
        {
            return await Mediator.Send(new CreateTreatment.Command { Treatment = treatment });
        }

        [HttpPut]
        public async Task<ActionResult> EditTreatment(Treatment treatment)
        {
        await Mediator.Send(new EditTreatment.Command { Treatment = treatment });
        return NoContent ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTreatment(string id)
        {
            await Mediator.Send(new DeleteTreatment.Command { Id = id });
            return Ok();
        }
    }
}
