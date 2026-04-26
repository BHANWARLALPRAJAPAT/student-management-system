using MediatR;
using StudentManagementSystem.Application.Students.DTOs;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;