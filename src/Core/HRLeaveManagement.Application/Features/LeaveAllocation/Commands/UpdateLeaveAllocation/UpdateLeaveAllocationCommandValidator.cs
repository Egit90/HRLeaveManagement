using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.NumberOfDays)
                .GreaterThan(0);
        RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year);

        RuleFor(p => p.LeaveTypeId)
                .NotNull()
                .NotEmpty()
                .MustAsync(LeaveTypeMustExist);

        RuleFor(p => p.Id)
                .MustAsync(LeaveAllocationMustExist)
                .NotNull();
    }


    private async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken token)
    {
        return await _leaveTypeRepository.GetByIdAsync(id) != null;
    }

    private async Task<bool> LeaveAllocationMustExist(Guid id, CancellationToken token)
    {
        return await _leaveAllocationRepository.GetByIdAsync(id) != null;
    }

}