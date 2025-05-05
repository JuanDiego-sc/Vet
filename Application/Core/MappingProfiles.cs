using System;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles(){

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

        CreateMap<Disease, Disease>();
        CreateMap<Medicine, Medicine>();
        CreateMap<Treatment, Treatment>();
        CreateMap<AppointmentDetail, AppointmentDetail>();
        
    }
}
