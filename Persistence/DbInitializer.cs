using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class DbInitializer
{
    public static async Task SeedData (AppDbContext context)
    {
        // Asegurarse de que la base de datos esté creada
        context.Database.EnsureCreated();

        // Si ya hay mascotas, asumimos que la base de datos ya fue inicializada
        if (context.Pets.Any()) return;

        // Crear algunas enfermedades
        var diseases = new List<Disease>
        {
            new Disease
            {
                Name = "Parvovirus",
                Type = "Viral",
                Description = "Enfermedad viral altamente contagiosa que afecta principalmente a cachorros",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            },
            new Disease
            {
                Name = "Otitis",
                Type = "Bacterial",
                Description = "Infección del oído común en perros y gatos",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.Diseases.AddRange(diseases);

        // Crear algunos medicamentos
        var medicines = new List<Medicine>
        {
            new Medicine
            {
                Name = "Amoxicilina",
                Stock = 100,
                Description = "Antibiótico de amplio espectro",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            },
            new Medicine
            {
                Name = "Antiparasitario",
                Stock = 50,
                Description = "Medicamento para el control de parásitos internos",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.Medicines.AddRange(medicines);

        // Crear algunas mascotas
        var pets = new List<Pet>
        {
            new Pet
            {
                PetName = "Luna",
                Breed = "Labrador",
                Species = "Perro",
                Gender = "Hembra",
                Birthdate = DateTime.UtcNow.AddYears(-2),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            },
            new Pet
            {
                PetName = "Milo",
                Breed = "Siamés",
                Species = "Gato",
                Gender = "Macho",
                Birthdate = DateTime.UtcNow.AddYears(-1),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.Pets.AddRange(pets);

        // Guardar cambios para tener IDs generados
        await context.SaveChangesAsync();

        // Crear algunas citas médicas
        var appointments = new List<MedicalAppointment>
        {
            new MedicalAppointment
            {
                AppointmentDate = DateTime.UtcNow.AddDays(-5),
                AppointmentStatus = Status.Closed,
                Reason = "Chequeo general",
                IdPet = pets[0].Id,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            },
            new MedicalAppointment
            {
                AppointmentDate = DateTime.UtcNow.AddDays(2),
                AppointmentStatus = Status.Pending,
                Reason = "Vacunación",
                IdPet = pets[1].Id,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.MedicalAppointments.AddRange(appointments);

        // Guardar cambios para tener IDs generados
        await context.SaveChangesAsync();

        // Crear detalles de citas
        var appointmentDetails = new List<AppointmentDetail>
        {
            new AppointmentDetail
            {
                Diagnostic = "Mascota saludable",
                Observation = "Continuar con plan de alimentación actual",
                IdDisease = diseases[0].Id,
                IdAppointment = appointments[0].Id,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.AppointmentDetails.AddRange(appointmentDetails);

        // Crear tratamientos
        var treatments = new List<Treatment>
        {
            new Treatment
            {
                Duration = 7,
                Dose = "1 pastilla cada 12 horas",
                Contraindications = "No administrar con el estómago vacío",
                IdMedicine = medicines[0].Id,
                IdDetail = appointmentDetails[0].Id,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        };
        context.Treatments.AddRange(treatments);

        // Guardar todos los cambios
        await context.SaveChangesAsync();
    }
}