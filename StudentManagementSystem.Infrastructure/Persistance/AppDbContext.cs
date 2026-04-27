using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(e => e.Course)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Age)
                  .IsRequired();

            entity.Property(e => e.CreatedDate)
                  .IsRequired();

            // Optional but recommended
            entity.HasIndex(e => e.Email)
                  .IsUnique();
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.PasswordHash)
                .IsRequired();

            entity.Property(e => e.RefreshToken)
                .HasMaxLength(500);

            entity.Property(e => e.RefreshTokenExpiryTime);
        });


    }
}