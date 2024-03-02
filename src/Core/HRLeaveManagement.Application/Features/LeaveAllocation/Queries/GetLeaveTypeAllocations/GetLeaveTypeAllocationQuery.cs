using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application;

public record GetLeaveTypeAllocationQuery : IRequest<List<LeaveAllocationDto>>;
