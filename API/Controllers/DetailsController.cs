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
    public async Task<ActionResult<List<AppointmentDetailDto>>> GetDetailList()
    {
        return await Mediator.Send(new GetDetailList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDetailDto>> GetDetail(string id)
    {
        return await Mediator.Send(new GetDetail.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateDetail(AppointmentDetailDto detail)
    {
        return await Mediator.Send(new CreateDetail.Command { AppointmentDetailDto = detail });
    }

    [HttpPut]
    public async Task<ActionResult> EditDetail(AppointmentDetailDto detail)
    {
       await Mediator.Send(new EditDetail.Command { AppointmentDetailDto = detail });
       return NoContent ();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDetail(string id)
    {
        await Mediator.Send(new DeleteDetail.Command { Id = id });
        return Ok();
    }
}

