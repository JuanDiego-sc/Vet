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
        public async Task<ActionResult<List<Medicine>>> GetMedicineList()
        {
            return await Mediator.Send(new GetMedicineList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(string id)
        {
            return await Mediator.Send(new GetMedicine.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateMedicine(Medicine medicine)
        {
            return await Mediator.Send(new CreateMedicine.Command { Medicine = medicine });
        }

        [HttpPut]
        public async Task<ActionResult> EditMedicine(Medicine medicine)
        {
        await Mediator.Send(new EditMedicine.Command { Medicine = medicine });
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
