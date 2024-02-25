using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
public class UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var LeaveType = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.UpdateAsync(LeaveType);

        return Unit.Value;
    }
}