using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery(Guid Id) : IRequest<LeaveRequestDetailDto>;