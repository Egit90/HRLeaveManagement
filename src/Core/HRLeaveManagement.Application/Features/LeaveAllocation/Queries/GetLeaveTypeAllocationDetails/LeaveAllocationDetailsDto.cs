using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application;

public class LeaveAllocationDetailsDto
{
    public int NumberOfDays { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Period { get; set; }
}

public class LeaveAllocationDetailHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IMapper mapper
) : IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository = leaveAllocationRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var allocation = await _leaveAllocationRepository.GetByIdAsync(request.id);
        var data = _mapper.Map<LeaveAllocationDetailsDto>(allocation);
        return data;
    }
}