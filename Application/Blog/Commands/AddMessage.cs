using System;
using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Blog.Commands;

public class AddMessage
{
    public class Command : IRequest<Result<MessageDto>>
    {
        public required string BlogId { get; set; }
        public required string BodyMessage { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor, IMapper mapper) 
        : IRequestHandler<Command, Result<MessageDto>>
    {
        public async Task<Result<MessageDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var blog = await context.Blogs
                .Include(m => m.Messages)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);

            if (blog == null) return Result<MessageDto>.Failure("Blog not found in the database", 400);

            var user = await userAccessor.GetUserAsync();

            var message = new Message
            {
                BodyMessage = request.BodyMessage,
                UserId = user.Id,
                BlogId = blog.Id
            };

            context.Messages.Add(message);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            return result
                ? Result<MessageDto>.Success(mapper.Map<MessageDto>(message))
                : Result<MessageDto>.Failure("Failed to add message in DB", 400);
        }
    }
}
