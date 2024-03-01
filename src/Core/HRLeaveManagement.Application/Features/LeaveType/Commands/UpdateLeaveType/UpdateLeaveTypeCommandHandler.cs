using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
public class UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<UpdateLeaveTypeCommandHandler> logger) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger = logger;

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validate 

        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (validatorResult.Errors.Count != 0)
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Domain.LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave Type", validatorResult);
        }


        var LeaveType = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.UpdateAsync(LeaveType);

        return Unit.Value;
    }
}