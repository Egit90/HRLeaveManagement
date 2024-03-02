using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
public record DeleteLeaveRequestCommand(Guid Id) : IRequest<Unit>;
