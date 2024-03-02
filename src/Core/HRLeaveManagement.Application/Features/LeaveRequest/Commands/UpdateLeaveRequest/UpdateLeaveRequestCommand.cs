using HRLeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? RequestComments { get; set; }
    public bool Cancelled { get; set; }
}
