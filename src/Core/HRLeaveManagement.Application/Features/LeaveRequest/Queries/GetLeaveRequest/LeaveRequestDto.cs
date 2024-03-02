using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequest;
public class LeaveRequestDto
{
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public LeaveTypeDto? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool? Approved { get; set; }

}