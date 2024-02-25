using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
public class CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid) throw new BadRequestException("Invalid Leave Type", validationResult);

        var newLeaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(newLeaveTypeToCreate);

        return newLeaveTypeToCreate.Id;
    }
}