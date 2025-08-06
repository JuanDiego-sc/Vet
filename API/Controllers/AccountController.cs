using System;
using System.Drawing;
using API.DTOs;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers;

public class AccountController(SignInManager<AppUser> signInManager, IMapper mapper) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = new User
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            Name = registerUserDto.Name,
            Password = registerUserDto.Password
        };

        var mappedUser = mapper.Map<AppUser>(user);
        var result = await signInManager.UserManager.CreateAsync(mappedUser, registerUserDto.Password);

        if (result.Succeeded) return Ok();

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem();
    }

    [AllowAnonymous]
    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        if (User.Identity?.IsAuthenticated == false) return NoContent();

        var user = await signInManager.UserManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        return Ok(new
        {
            user.DisplayName,
            user.Email,
            user.Id
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return NoContent();
    }
}
