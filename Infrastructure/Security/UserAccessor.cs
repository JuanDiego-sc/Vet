using System;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Entities;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext context, IMapper mapper)
    : IUserAccessor
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("User not found");
    }
    public async Task<AppUser> GetUserAsync()
    {
        var user = await context.Users.FindAsync(GetUserId());
        var mappedUser = mapper.Map<AppUser>(user);
        
        return mappedUser
            ?? throw new UnauthorizedAccessException("User not logged in");
    }
}
