using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Auth.DTOs;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.API.Services;
using StudentManagementSystem.Domain.Entities;
using BCrypt.Net;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(IAppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
        {
            return Unauthorized(new
            {
                statusCode = 401,
                message = "Invalid email or password"
            });
        }

        // verify password
        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            return Unauthorized(new
            {
                statusCode = 401,
                message = "Invalid email or password"
            });
        }

        var token = _jwtService.GenerateToken(user.Email);

        return Ok(new
        {
            statusCode = 200,
            message = "Login successful",
            result = token
        });
    }
}