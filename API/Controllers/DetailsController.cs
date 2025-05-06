using Application.AppointmentDetails.Commands;
using Application.AppointmentDetails.Queries;
using Application.DTOs;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

public class DetailsController(AppDbContext context) : BaseApiController
{
        private readonly AppDbContext context = context;

    [HttpGet]	
    //Good practice to use async/await for database queries
    public async Task<ActionResult<List<AppointmentDetail>>> GetActivities()
    {
        return await Mediator.Send(new GetDetailList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDetail>> GetActivityDetail(string id)
    {
        return await Mediator.Send(new GetDetail.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(AppointmentDetail detail)
    {
        return await Mediator.Send(new CreateDetail.Command { AppointmentDetail = detail });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(AppointmentDetail detail)
    {
       await Mediator.Send(new EditDetail.Command { AppointmentDetail = detail });
       return NoContent ();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteDetail.Command { Id = id });
        return Ok();
    }
}

