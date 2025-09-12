using System;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Blog.Queries;

public class GetMessagesList
{
    public class Query : IRequest<Result<List<MessageDto>>>
    {
        public required string BlogId { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper)
        : IRequestHandler<Query, Result<List<MessageDto>>>
    {
        public async Task<Result<List<MessageDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var messages = await context.Messages
                .Where(b => b.BlogId == request.BlogId)
                .OrderByDescending(c => c.CreatedAt)
                .ProjectTo<MessageDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<List<MessageDto>>.Success(messages);
        }
    }
}
