namespace Application.DTOs
{
    public class DiseaseAnalysisDto
    {
        public string DiseaseName { get; set; } = "";
        public int CaseCount { get; set; }
        public List<string>? UsedMedicines { get; set; }
        public bool HasStockIssues { get; set; }
        public List<MedicalStockAlertDto>? StockAlerts { get; set; }
    }
} 