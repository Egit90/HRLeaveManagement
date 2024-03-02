using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
                            ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);

        leaveRequest.Cancelled = true;

        try
        {

            var email = new EmailMessage
            {
                To = string.Empty, //Get Email from employee records
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been Cancelled successfully",
                Subject = "Leave Request Cancelled"
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