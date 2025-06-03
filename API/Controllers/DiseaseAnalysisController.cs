using Application.DiseaseAnalysis.Queries;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DiseaseAnalysisController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("analyze")]
        public async Task<ActionResult<List<DiseaseAnalysisDto>>> AnalyzeDiseasePatterns(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var startDateUtc = startDate.Kind == DateTimeKind.Unspecified 
                ? DateTime.SpecifyKind(startDate, DateTimeKind.Utc) 
                : startDate.ToUniversalTime();
                
             var endDateUtc = endDate.Kind == DateTimeKind.Unspecified 
                ? DateTime.SpecifyKind(endDate, DateTimeKind.Utc) 
                : endDate.ToUniversalTime();
                
            try
            {
                var query = new GetDiseaseAnalysis.Query
                {
                    StartDate = startDateUtc,
                    EndDate = endDateUtc
                };

                var results = await _mediator.Send(query);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al analizar patrones de enfermedades", error = ex.Message });
            }
        }
    }
} 