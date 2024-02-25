using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>;
