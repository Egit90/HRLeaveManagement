using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));

        RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{Property Name} must be present");
    }

    private async Task<bool> LeaveRequestMustExist(Guid guid, CancellationToken token)
    {
        return await _leaveRequestRepository.GetByIdAsync(guid) != null;
    }
}