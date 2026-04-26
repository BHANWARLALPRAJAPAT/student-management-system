using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Domain.Entities;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IAppDbContext _context;

    public UpdateStudentHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken);

        if (student == null) return false;

        student.Name = request.Name;
        student.Email = request.Email;
        student.Age = request.Age;
        student.Course = request.Course;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}