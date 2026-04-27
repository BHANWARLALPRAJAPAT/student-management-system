using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.Students.DTOs;
using StudentManagementSystem.Application.Common.Responses;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetStudentsQuery());

        return Ok(ApiResponse<List<StudentDto>>.SuccessResponse(
            result,
            "Students retrieved successfully"
        ));
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id));

        if (result == null)
            return NotFound(ApiResponse<string>.FailResponse("Student not found", 404));

        return Ok(ApiResponse<StudentDto>.SuccessResponse(
            result,
            "Student retrieved successfully"
        ));
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<int>.SuccessResponse(
            id,
            "Student created successfully",
            201
        ));
    }
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateStudentCommand command)
    {
        var success = await _mediator.Send(command);

        if (!success)
            return NotFound(ApiResponse<string>.FailResponse("Student not found", 404));

        return Ok(ApiResponse<string>.SuccessResponse(
            "Updated successfully",
            "Student updated successfully"
        ));
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteStudentCommand(id));

        if (!success)
            return NotFound(ApiResponse<string>.FailResponse("Student not found", 404));

        return Ok(ApiResponse<string>.SuccessResponse(
            "Deleted successfully",
            "Student deleted successfully"
        ));
    }
}