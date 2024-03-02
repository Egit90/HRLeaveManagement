using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var validatorRes = validator.Validate(request);

        if (validatorRes.Errors.Count != 0)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
        await _leaveAllocationRepository.DeleteAsync(leaveAllocation!);

        return Unit.Value;
    }

}
