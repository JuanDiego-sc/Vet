using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class PetController(AppDbContext context) : BaseApiController
    {

    }
}
