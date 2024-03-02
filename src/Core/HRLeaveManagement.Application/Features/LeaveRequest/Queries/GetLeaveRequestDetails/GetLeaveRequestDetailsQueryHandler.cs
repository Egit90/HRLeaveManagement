using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }
    public async Task<LeaveRequestDetailDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var data = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        var dto = _mapper.Map<LeaveRequestDetailDto>(data);
        return dto;
    }
}