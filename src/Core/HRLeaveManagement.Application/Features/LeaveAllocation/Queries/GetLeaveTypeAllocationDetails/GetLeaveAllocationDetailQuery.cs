using MediatR;

namespace HRLeaveManagement.Application;

public record GetLeaveAllocationDetailQuery(Guid id) : IRequest<LeaveAllocationDetailsDto>;