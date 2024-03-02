using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.Shared;
using HRLeaveManagement.Application.Models.Email;
using HRLeaveManagement.Domain;
using MediatR;
using src.Core.Exceptions;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public string? RequestComments { get; set; }
}

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

    public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository,
                                            ILeaveRequestRepository leaveRequestRepository,
                                            IMapper mapper,
                                            IEmailSender emailSender,
                                            IAppLogger<CreateLeaveRequestCommandHandler> appLogger
                                            )

    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validatorRes = await validator.ValidateAsync(request, cancellationToken);

        if (validatorRes.Errors.Count != 0)
        {
            throw new BadRequestException("Leave request not valid", validatorRes);
        }

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {

            var email = new EmailMessage
            {
                To = string.Empty, //Get Email from employee records
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been Created successfully",
                Subject = "Leave Request Created"
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

