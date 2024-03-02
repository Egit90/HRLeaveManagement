using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application;

public class GetLeaveTypeAllocationQueryHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IMapper mapper
) : IRequestHandler<GetLeaveTypeAllocationQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository = leaveAllocationRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveTypeAllocationQuery request, CancellationToken cancellationToken)
    {
        var allocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
        var data = _mapper.Map<List<LeaveAllocationDto>>(allocations);
        return data;
    }
}