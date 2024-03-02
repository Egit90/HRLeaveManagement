using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application;

public class DeleteLeaveAllocationCommandValidator : AbstractValidator<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.Id)
                .MustAsync(AllocationExist)
                .NotNull()
                .NotEmpty();
    }

    private async Task<bool> AllocationExist(Guid guid, CancellationToken token)
    {
        return await _leaveAllocationRepository.GetByIdAsync(guid) != null;
    }
}