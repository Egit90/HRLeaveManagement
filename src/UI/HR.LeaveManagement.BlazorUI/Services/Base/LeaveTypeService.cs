using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class LeaveTypeService(IClient client, IMapper mapper) : BaseHttpService(client), ILeaveTypeService
{
    private readonly IMapper _mapper = mapper;

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            var command = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPOSTAsync(command);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(Guid id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(new() { Id = id });
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetail(Guid id)
    {

        var leaveType = await _client.GetLeaveTypeByIDAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        var leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<Response<Guid>> UpdateLeaveType(Guid id, LeaveTypeVM leaveType)
    {
        try
        {
            var command = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            command.Id = id;
            await _client.LeaveTypesPUTAsync(command);
            return new() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }

    }
}
