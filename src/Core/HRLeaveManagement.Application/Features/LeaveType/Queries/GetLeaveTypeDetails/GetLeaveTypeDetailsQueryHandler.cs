using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
public class GetLeaveTypeDetailsQueryHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(LeaveType), request.Id);

        var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

        return data;
    }
}