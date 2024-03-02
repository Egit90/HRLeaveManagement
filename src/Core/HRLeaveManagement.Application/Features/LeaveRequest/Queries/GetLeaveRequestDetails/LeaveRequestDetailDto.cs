using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
public class LeaveRequestDetailDto
{

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public Guid LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}
