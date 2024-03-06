using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    public List<LeaveTypeVM> LeaveTypeList { get; set; }
    public string Message { get; set; } = string.Empty;
    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leaveTypes/create/");
    }
    protected void DetailsLeaveType(Guid id)
    {
        NavigationManager.NavigateTo($"/leaveTypes/details/{id}");
    }
    protected async Task DeleteLeaveType(Guid id)
    {
        var res = await LeaveTypeService.DeleteLeaveType(id);

        if (res.Success) StateHasChanged();
        else Message = res.Message;

    }
    protected void EditLeaveType(Guid id)
    {
        NavigationManager.NavigateTo($"/leaveTypes/edit/{id}");
    }
    protected void AllocateLeaveType(Guid id)
    {
        throw new NotImplementedException();
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypeList = await LeaveTypeService.GetLeaveTypes();
    }
}