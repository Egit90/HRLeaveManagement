using AutoMapper;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        // Allocation
        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>().ReverseMap();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }

}
