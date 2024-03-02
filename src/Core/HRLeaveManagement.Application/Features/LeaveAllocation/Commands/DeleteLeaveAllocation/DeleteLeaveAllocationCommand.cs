using MediatR;

namespace HRLeaveManagement.Application;

public class DeleteLeaveAllocationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
