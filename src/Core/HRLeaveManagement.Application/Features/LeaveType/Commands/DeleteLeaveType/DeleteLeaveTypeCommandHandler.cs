using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(LeaveType), request.Id);
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        return Unit.Value;
    }
}