using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this._leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("Name must be fewer than 70");

        RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{Property Name} cannot exceed 100")
                .GreaterThan(1);

        RuleFor(p => p)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");
    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}