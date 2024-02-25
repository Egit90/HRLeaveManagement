namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
public class LeaveTypeDetailDto
{
    public Guid Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }

}