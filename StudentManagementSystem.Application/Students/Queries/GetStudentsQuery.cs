using MediatR;
using StudentManagementSystem.Application.Students.DTOs;

public record GetStudentsQuery() : IRequest<List<StudentDto>>;