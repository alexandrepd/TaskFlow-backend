using FluentValidation;
using TaskFlow.Application.Features.Tasks.Commands;

namespace TaskFlow.Application.Features.Tasks.Validators;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    private static readonly string[] ValidPriorities = { "Low", "Medium", "High" };

    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Category)
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .Must(p => ValidPriorities.Contains(p))
            .WithMessage("Priority must be Low, Medium, or High.");
    }
}
