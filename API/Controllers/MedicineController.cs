using Application.DTOs;
using Application.Medicines.Commands;
using Application.Medicines.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class MedicineController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext context = context;


        [HttpGet]	
        //Good practice to use async/await for database queries
        public async Task<ActionResult<List<MedicineDto>>> GetMedicineList()
        {
            return await Mediator.Send(new GetMedicineList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetMedicine(string id)
        {
            return HandleResult(await Mediator.Send(new GetMedicine.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateMedicine(MedicineDto medicine)
        {
            return HandleResult(await Mediator.Send(new CreateMedicine.Command { MedicineDto = medicine }));
        }

        [HttpPut]
        public async Task<ActionResult> EditMedicine(MedicineDto medicine)
        {
        await Mediator.Send(new EditMedicine.Command { MedicineDto = medicine });
        return NoContent ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicine(string id)
        {
            await Mediator.Send(new DeleteMedicine.Command { Id = id });
            return Ok();
        }
    }
}
