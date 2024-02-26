using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository(HrDataBaseContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, Guid leaveTypeid, int period)
    {
        return await _context.LeaveAllocations
                             .AnyAsync(q =>
                             q.LeaveTypeId == leaveTypeid &&
                             q.Period == period &&
                             q.EmployeeId == userId);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        return await _context.LeaveAllocations.Include(x => x.LeaveType).ToListAsync();
    }
    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(Guid id)
    {
        return await _context.LeaveAllocations
                             .Include(x => x.LeaveType)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        return await _context.LeaveAllocations
                             .Where(x => x.EmployeeId == userId)
                             .ToListAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, Guid leaveTypeid)
    {
        return await _context.LeaveAllocations
                             .FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeid);
    }
}