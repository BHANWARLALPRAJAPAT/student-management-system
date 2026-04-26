using MediatR;
public record UpdateStudentCommand(
    int Id,
    string Name,
    string Email,
    int Age,
    string Course
) : IRequest<bool>;