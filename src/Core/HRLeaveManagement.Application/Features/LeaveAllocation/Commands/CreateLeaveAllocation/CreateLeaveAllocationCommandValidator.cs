using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        RuleFor(p => p.LeaveTypeId)
                .NotEmpty()
                .NotNull()
                .MustAsync(LeaveTypeMustExist);
    }

    private async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken cancellationToken)
    {
        return await _leaveAllocationRepository.GetByIdAsync(id) != null;
    }

}