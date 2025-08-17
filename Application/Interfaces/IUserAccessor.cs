using System;
using Domain;
using Persistence.Entities;

namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetUserId();
    Task<AppUser> GetUserAsync();
}
