using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Infrastructure.Persistence;
using StudentManagementSystem.Application;
using StudentManagementSystem.Application.Common.Interfaces;
using MediatR;
using FluentValidation;
using StudentManagementSystem.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Services
builder.Services.AddControllers();

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Interface mapping
builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

// 🔹 MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

// 🔹 FluentValidation (IMPORTANT)
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(StudentManagementSystem.Application.Common.Behaviors.ValidationBehavior<,>)
);
// builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// 🔹 Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();