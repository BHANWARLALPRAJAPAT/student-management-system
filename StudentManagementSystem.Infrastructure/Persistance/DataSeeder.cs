using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
            return; // already seeded

        var user = new User
        {
            Email = "admin@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}