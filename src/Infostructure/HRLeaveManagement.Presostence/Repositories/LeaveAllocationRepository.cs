using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository(HrDataBaseContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
}