using System;
using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.AppointmentDetails.Commands;

public class CreateDetail
{
    public class Command : IRequest<string>
    {
        public required AppointmentDetailDto AppointmentDetailDto {get; set;}
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var detail = mapper.Map<AppointmentDetail>(request.AppointmentDetailDto);
            context.AppointmentDetails.Add(detail);
            await context.SaveChangesAsync(cancellationToken);
            return detail.Id;
        }
    }
}
