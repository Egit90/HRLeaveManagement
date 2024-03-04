using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTest
{
    private HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreateValue()
    {
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }
}