using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Commands;

public class EditDetail
{
    public class Command : IRequest{
        public required AppointmentDetailDto AppointmentDetailDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = await context.AppointmentDetails
                .FindAsync([request.AppointmentDetailDto.Id], cancellationToken) 
             ?? 
            throw new Exception("Appointment detail not found");

            mapper.Map(request.AppointmentDetailDto, detail); 
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
