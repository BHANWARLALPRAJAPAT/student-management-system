using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Auth.DTOs;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Domain.Entities;
using BCrypt.Net;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAppDbContext context,
        IJwtService jwtService,
        ILogger<AuthController> logger)
    {
        _context = context;
        _jwtService = jwtService;
        _logger = logger;
    }

    // ================= LOGIN =================
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var accessToken = _jwtService.GenerateJwtToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Login successful for email: {Email}", user.Email);

        return Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600
        });
    }

    // ================= REFRESH TOKEN =================
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        _logger.LogInformation("Refresh token request received");

        var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

        if (principal == null)
        {
            _logger.LogWarning("Invalid access token in refresh request");
            return Unauthorized("Invalid access token");
        }

        var email = principal.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("Email claim missing in token");
            return Unauthorized("Invalid token");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null ||
            user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            _logger.LogWarning("Invalid refresh token for email: {Email}", email);
            return Unauthorized("Invalid refresh token");
        }

        var newAccessToken = _jwtService.GenerateJwtToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Refresh token successful for email: {Email}", email);

        return Ok(new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = 3600
        });
    }
}