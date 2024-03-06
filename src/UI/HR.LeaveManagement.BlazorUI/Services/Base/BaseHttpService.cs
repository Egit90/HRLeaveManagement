namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService(IClient client)
{
    protected IClient _client = client;
    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        return ex.StatusCode switch
        {
            400 => new Response<Guid>() { Message = "Invalid Data was submitted", ValidationErrors = ex.Response, Success = false },
            404 => new Response<Guid>() { Message = "The Record was not found", ValidationErrors = ex.Response, Success = false },
            _ => new Response<Guid>() { Message = "Something went wrong, please try again", ValidationErrors = ex.Response, Success = false },
        };
    }
}

