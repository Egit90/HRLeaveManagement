using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
public record CancelLeaveRequestCommand(Guid Id) : IRequest<Unit>;
