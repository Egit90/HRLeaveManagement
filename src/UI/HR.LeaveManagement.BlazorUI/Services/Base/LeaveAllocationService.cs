using HR.LeaveManagement.BlazorUI.Contracts;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class LeaveAllocationService(IClient client) : BaseHttpService(client), ILeaveAllocationService
{
}