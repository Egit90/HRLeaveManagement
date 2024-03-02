using System.Data;
using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public record ChangeLeaveRequestApprovalCommand(Guid Id, bool Approved) : IRequest<Unit>;


public class ChangeLeaveRequestApprovalCommandHandler(
    IMapper mapper, IEmailSender emailSender,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger
    ) : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper = mapper;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger = appLogger;

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
                            ?? throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

        var validator = new ChangeLeaveRequestApprovalCommandValidator();

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {

            var email = new EmailMessage
            {
                To = string.Empty, //Get Email from employee records
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated",
                Subject = "Leave Request updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _appLogger.LogWarning($"Email send failed  => {ex.Message}");
        }

        return Unit.Value;
    }
}