using FluentValidation;

namespace StudentManagementSystem.Application.Students.Commands.UpdateStudent;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        // Id must be valid
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Student Id must be greater than 0");

        // Name validation
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters");

        // Email validation
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        // Age validation
        RuleFor(x => x.Age)
            .InclusiveBetween(5, 100)
            .WithMessage("Age must be between 5 and 100");

        // Course validation
        RuleFor(x => x.Course)
            .NotEmpty()
            .WithMessage("Course is required")
            .MaximumLength(100)
            .WithMessage("Course cannot exceed 100 characters");
    }
}