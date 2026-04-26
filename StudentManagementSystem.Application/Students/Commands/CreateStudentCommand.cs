using MediatR;
public record CreateStudentCommand(
    string Name,
    string Email,
    int Age,
    string Course
) : IRequest<int>;