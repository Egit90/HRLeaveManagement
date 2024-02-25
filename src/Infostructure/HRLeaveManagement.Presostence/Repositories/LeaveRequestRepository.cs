using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDataBaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{

}
