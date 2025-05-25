using System;

namespace Application.DTOs;

public class MedicalStockAlertDto
{
    public string MedicineName { get; set; } = "";
    public int CurrentStock { get; set; }
    public int RequiredStock { get; set; }
    
}
