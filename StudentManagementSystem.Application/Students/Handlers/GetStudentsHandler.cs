using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Application.Students.DTOs;

public class GetStudentsHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly IAppDbContext _context;

    public GetStudentsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Students
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                Course = s.Course
            })
            .ToListAsync(cancellationToken);
    }
}