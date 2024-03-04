using AutoMapper;
using HR.LeaveManagement.Application.UnitTest.Mocks;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTest.Features.LeaveTypes.Queries;

public class GetLeaveTypeQueryHandlerTest
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypeQueryHandler>> _appLogger;

    public GetLeaveTypeQueryHandlerTest()
    {
        _mockRepo = MocLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _appLogger = new Mock<IAppLogger<GetLeaveTypeQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypeQueryHandler(_mapper, _mockRepo.Object, _appLogger.Object);
        var res = await handler.Handle(new GetLeaveTypeQuery(), CancellationToken.None);

        res.ShouldBeOfType<List<LeaveTypeDto>>();
        res.Count.ShouldBe(3);


    }
}