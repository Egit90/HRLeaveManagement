using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {

        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate)
                .WithMessage("{Property Name} must be before {ComparisonValue}");

        RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate)
                .WithMessage("{Property Name} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
                .NotNull()
                .NotEmpty()
                .MustAsync(LeaveTypeMustAsync);
    }

    private async Task<bool> LeaveTypeMustAsync(Guid guid, CancellationToken token)
    {
        return await _leaveTypeRepository.GetByIdAsync(guid) != null;
    }
}