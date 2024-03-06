using HR.LeaveManagement.BlazorUI.Contracts;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class LeaveRequestService(IClient client) : BaseHttpService(client), ILeaveRequestService
{
}
