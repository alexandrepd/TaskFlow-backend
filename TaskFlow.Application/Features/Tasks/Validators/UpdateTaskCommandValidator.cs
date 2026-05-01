using FluentValidation;
using TaskFlow.Application.Features.Tasks.Commands;

namespace TaskFlow.Application.Features.Tasks.Validators;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    private static readonly string[] ValidPriorities = { "Low", "Medium", "High" };
    private static readonly string[] ValidStatuses = { "Pending", "InProgress", "Completed", "Cancelled" };

    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.");

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

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => ValidStatuses.Contains(s))
            .WithMessage("Status must be Pending, InProgress, Completed, or Cancelled.");
    }
}
