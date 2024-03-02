using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveAllocationRepository, _leaveTypeRepository);
        var validatorRes = await validator.ValidateAsync(request, cancellationToken);

        if (validatorRes.Errors.Count != 0)
        {
            throw new BadRequestException("Invalid Leave Allocation", validatorRes);
        }



        var allocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id)
                        ?? throw new NotFoundException(nameof(LeaveAllocation), request.Id);


        _mapper.Map(request, allocation);

        await _leaveAllocationRepository.UpdateAsync(allocation);

        return Unit.Value;
    }
}