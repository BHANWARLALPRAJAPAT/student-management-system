using StudentManagementSystem.Domain.Entities;
using System.Security.Claims;

namespace StudentManagementSystem.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}