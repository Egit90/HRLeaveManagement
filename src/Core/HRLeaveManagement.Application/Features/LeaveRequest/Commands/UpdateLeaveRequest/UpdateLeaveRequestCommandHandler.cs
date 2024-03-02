using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(
                                                ILeaveRequestRepository leaveRequestRepository,
                                                ILeaveTypeRepository leaveTypeRepository,
                                                IMapper mapper,
                                                IEmailSender emailSender,
                                                IAppLogger<UpdateLeaveRequestCommandHandler> appLogger
                                           )
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
                             ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);


        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validatorRes = await validator.ValidateAsync(request, cancellationToken);

        if (validatorRes.Errors.Count != 0)
        {
            throw new BadRequestException("Invalid Leave Request", validatorRes);
        }



        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {

            var email = new EmailMessage
            {
                To = string.Empty, //Get Email from employee records
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully",
                Subject = "Leave Request Updated"
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