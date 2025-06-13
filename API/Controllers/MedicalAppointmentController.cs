using Application.DTOs;
using Application.MedicalAppointments.Commands;
using Application.MedicalAppointments.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class MedicalAppointmentController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext context = context;

        [HttpGet]	
        //Good practice to use async/await for database queries
        public async Task<ActionResult<List<MedicalAppointmentDto>>> GetAppointmentList()
        {
            return await Mediator.Send(new GetAppointmentList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalAppointmentDto>> GetAppointment(string id)
        {
            return await Mediator.Send(new GetAppointment.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAppointment(MedicalAppointmentDto appointment)
        {
            return await Mediator.Send(new CreateAppointment.Command { MedicalAppointmentDto = appointment });
        }

        [HttpPut]
        public async Task<ActionResult> EditAppointment(MedicalAppointmentDto appointment)
        {
        await Mediator.Send(new EditAppointment.Command { MedicalAppointmentDto = appointment });
        return NoContent ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(string id)
        {
            await Mediator.Send(new DeleteAppointment.Command { Id = id });
            return Ok();
        }
    }
}
