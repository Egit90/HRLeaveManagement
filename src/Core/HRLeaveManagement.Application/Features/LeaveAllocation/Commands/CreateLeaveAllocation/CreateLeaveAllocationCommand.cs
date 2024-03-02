using MediatR;

namespace HRLeaveManagement.Application;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public Guid LeaveTypeId { get; set; }
}
