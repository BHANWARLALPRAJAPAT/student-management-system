using MediatR;
using StudentManagementSystem.Application.Common.Interfaces;
using StudentManagementSystem.Domain.Entities;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateStudentHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Course = request.Course,
            CreatedDate = DateTime.UtcNow
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return student.Id;
    }
}