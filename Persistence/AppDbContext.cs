using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Pet> Pets {get; set;}
    public required DbSet<Medicine> Medicines {get; set;}
    public required DbSet<Disease> Diseases {get; set;}
    public required DbSet<MedicalAppointment> MedicalAppointments {get; set;}
    public required DbSet<Treatment> Treatments {get; set;}
    public required DbSet<AppointmentDetail> AppointmentDetails {get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de claves primarias
        modelBuilder.Entity<Pet>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Medicine>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Disease>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<MedicalAppointment>()
            .HasKey(ma => ma.Id);

        modelBuilder.Entity<Treatment>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<AppointmentDetail>()
            .HasKey(ad => ad.Id);

        // Pet - MedicalAppointment (One to Many)
        modelBuilder.Entity<MedicalAppointment>()
            .HasOne(ma => ma.Pet)
            .WithMany(p => p.MedicalAppointments)
            .HasForeignKey(ma => ma.IdPet)
            .IsRequired();

        // MedicalAppointment - AppointmentDetail (One to Many)
        modelBuilder.Entity<AppointmentDetail>()
            .HasOne(ad => ad.MedicalAppointment)
            .WithMany(ma => ma.AppointmentDetails)
            .HasForeignKey(ad => ad.IdAppointment)
            .IsRequired();

        // Disease - AppointmentDetail (One to Many)
        modelBuilder.Entity<AppointmentDetail>()
            .HasOne(ad => ad.Disease)
            .WithMany(d => d.AppointmentDetails)
            .HasForeignKey(ad => ad.IdDisease)
            .IsRequired();

        // Medicine - Treatment (One to Many)
        modelBuilder.Entity<Treatment>()
            .HasOne(t => t.Medicine)
            .WithMany(m => m.Treatments)
            .HasForeignKey(t => t.IdMedicine)
            .IsRequired();

        // AppointmentDetail - Treatment (One to Many)
        modelBuilder.Entity<Treatment>()
            .HasOne(t => t.AppointmentDetail)
            .WithMany(ad => ad.Treatments)
            .HasForeignKey(t => t.IdDetail)
            .IsRequired();
    }
}
