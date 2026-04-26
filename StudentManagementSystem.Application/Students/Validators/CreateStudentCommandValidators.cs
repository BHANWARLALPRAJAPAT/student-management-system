using FluentValidation;

namespace StudentManagementSystem.Application.Students.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
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