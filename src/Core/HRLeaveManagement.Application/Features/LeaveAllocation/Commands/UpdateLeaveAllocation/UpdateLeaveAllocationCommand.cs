using System.Data;
using MediatR;

namespace HRLeaveManagement.Application;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public int NumberOfDays { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Period { get; set; }
}
