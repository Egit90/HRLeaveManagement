using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
public record GetLeaveTypeDetailsQuery(Guid Id) : IRequest<LeaveTypeDetailDto>;
