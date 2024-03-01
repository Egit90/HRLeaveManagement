using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDatabaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return _context.LeaveRequests
                        .Include(c => c.LeaveType)
                        .ToListAsync();
    }

    public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        return _context.LeaveRequests
                        .Where(a => a.RequestingEmployeeId == userId)
                        .Include(c => c.LeaveType)
                        .ToListAsync();

    }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(Guid id)
    {
        return await _context.LeaveRequests
                            .Include(x => x.LeaveType)
                            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
