using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Application.Students.DTOs;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IAppDbContext _context;

    public GetStudentByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
            .Where(s => s.Id == request.Id)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                Course = s.Course
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}