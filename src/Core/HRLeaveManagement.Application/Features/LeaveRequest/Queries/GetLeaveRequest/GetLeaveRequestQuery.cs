using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequest;

public record class GetLeaveRequestQuery : IRequest<List<LeaveRequestDto>>;
