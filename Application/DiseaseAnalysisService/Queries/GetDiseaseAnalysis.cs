using Application.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.DiseaseAnalysis.Queries
{
    public class GetDiseaseAnalysis
    {
        public class Query : IRequest<List<DiseaseAnalysisDto>>
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class Handler(AppDbContext _context, IMapper _mapper) : IRequestHandler<Query, List<DiseaseAnalysisDto>>
        {
            private readonly AppDbContext _context = _context;
            private readonly IMapper _mapper = _mapper;

            public async Task<List<DiseaseAnalysisDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Obtener todas las citas médicas en el rango de fechas
                var appointments = await _context.MedicalAppointments
                    .Include(a => a.AppointmentDetails)
                        .ThenInclude(ad => ad.Disease)
                    .Include(a => a.AppointmentDetails)
                        .ThenInclude(ad => ad.Treatments)
                            .ThenInclude(t => t.Medicine)
                    .Where(a => a.AppointmentDate >= request.StartDate && a.AppointmentDate <= request.EndDate)
                    .ToListAsync(cancellationToken);

                // Agrupar por enfermedad y contar ocurrencias
                var diseaseGroups = appointments
                    .SelectMany(a => a.AppointmentDetails)
                    .GroupBy(ad => ad.Disease)
                    .Where(g => g.Count() >= 3)
                    .ToList();

                var analysisResults = new List<DiseaseAnalysisDto>();

                foreach (var group in diseaseGroups)
                {
                    var disease = group.Key;
                    var caseCount = group.Count();
                    var usedMedicines = group
                        .SelectMany(ad => ad.Treatments.Select(t => t.Medicine))
                        .Distinct()
                        .ToList();

                    var stockAlerts = new List<MedicalStockAlertDto>();

                    foreach (var medicine in usedMedicines)
                    {
                        if (medicine.Stock < caseCount)
                        {
                            stockAlerts.Add(new MedicalStockAlertDto
                            {
                                MedicineName = medicine.Name,
                                CurrentStock = medicine.Stock,
                                RequiredStock = caseCount
                            });
                        }
                    }

                    analysisResults.Add(new DiseaseAnalysisDto
                    {
                        DiseaseName = disease.Name,
                        CaseCount = caseCount,
                        UsedMedicines = usedMedicines.Select(m => m.Name).ToList(),
                        HasStockIssues = stockAlerts.Any(),
                        StockAlerts = stockAlerts
                    });
                }

                return analysisResults;
            }
        }
    }
} 