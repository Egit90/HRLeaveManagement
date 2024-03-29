using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
public class GetLeaveTypeQueryHandler(
                                        IMapper mapper,
                                        ILeaveTypeRepository leaveTypeRepository,
                                        IAppLogger<GetLeaveTypeQueryHandler> logger
                                        ) : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypeQueryHandler> _logger = logger;

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetAsync();
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveType);

        _logger.LogInformation("Leave types were retrieve successfully");
        return data;
    }
}