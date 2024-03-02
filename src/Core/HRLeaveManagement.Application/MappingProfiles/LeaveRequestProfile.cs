using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveRequest.GetLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.GetLeaveRequestDetails;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>();
        CreateMap<LeaveRequest, LeaveRequestDetailDto>();

    }
}
