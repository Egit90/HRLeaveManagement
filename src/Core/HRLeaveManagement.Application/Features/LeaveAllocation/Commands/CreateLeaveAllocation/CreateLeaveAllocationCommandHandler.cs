using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var res = await validator.ValidateAsync(request, cancellationToken);

        if (res.Errors.Count != 0)
        {
            throw new BadRequestException("Invalid leave allocation request", res);
        }

        var allocation = _leaveAllocationRepository.GetByIdAsync(request.LeaveTypeId);


        var leaveAllocation = _mapper.Map<LeaveAllocation>(request);

        await _leaveAllocationRepository.CreateAsync(leaveAllocation);

        return Unit.Value;

    }
}
