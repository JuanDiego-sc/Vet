using System;
using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Pet, PetDto>()
        .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src =>
                src.Birthdate.Kind == DateTimeKind.Utc
                    ? src.Birthdate
                    : DateTime.SpecifyKind(src.Birthdate, DateTimeKind.Utc)));

        CreateMap<MedicalAppointment, MedicalAppointmentDto>()
        .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src =>
                src.AppointmentDate.Kind == DateTimeKind.Utc
                    ? src.AppointmentDate
                    : DateTime.SpecifyKind(src.AppointmentDate, DateTimeKind.Utc)))
        .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet.PetName));;

        CreateMap<Disease, DiseaseDto>();
        CreateMap<Medicine, MedicineDto>();
        CreateMap<Treatment, TreatmentDto>()
        .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name));
        CreateMap<AppointmentDetail, AppointmentDetailDto>()
        .ForMember(dest => dest.DiseaseName, opt => opt.MapFrom(src => src.Disease.Name));


        CreateMap<Pet, Pet>()
        .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src =>
         src.Birthdate.Kind == DateTimeKind.Utc
            ? src.Birthdate
            : DateTime.SpecifyKind(src.Birthdate, DateTimeKind.Utc)));

        CreateMap<MedicalAppointment, MedicalAppointment>()
        .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src =>
        src.AppointmentDate.Kind == DateTimeKind.Utc
             ? src.AppointmentDate
             : DateTime.SpecifyKind(src.AppointmentDate, DateTimeKind.Utc)));

        CreateMap<AppointmentDetail, AppointmentDetail>();
        CreateMap<Disease, Disease>();
        CreateMap<Medicine, Medicine>();
        CreateMap<Treatment, Treatment>();
        

        
    }
}
