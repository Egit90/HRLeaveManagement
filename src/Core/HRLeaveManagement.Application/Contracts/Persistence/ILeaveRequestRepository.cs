using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetails(Guid id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
}