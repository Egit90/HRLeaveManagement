using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTest.Mocks;

public class MocLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()

    {
        var LeaveTypes = new List<LeaveType>
        {
            new() {
                Id = Guid.NewGuid(),
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new(){
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new(){
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "test Maternity"
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(LeaveTypes);
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    LeaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

        return mockRepo;
    }
}