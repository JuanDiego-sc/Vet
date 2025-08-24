using System.Security.Claims;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    : IUserAccessor
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("User not found");
    }
    public async Task<User> GetUserAsync()
    {
        var user = await context.Users.FindAsync(GetUserId());
        return user
            ?? throw new UnauthorizedAccessException("User not logged in");
    }
}
