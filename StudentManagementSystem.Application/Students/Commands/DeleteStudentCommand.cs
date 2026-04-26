using MediatR;
public record DeleteStudentCommand(int Id) : IRequest<bool>;