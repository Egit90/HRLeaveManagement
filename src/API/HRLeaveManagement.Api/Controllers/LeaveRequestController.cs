
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers;
public static class LeaveRequestController
{
    public static void MapLeaveRequestController(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/LeaveRequest").WithOpenApi();
        group.MapGet("", Get);
        group.MapGet("{ID}", GetById).WithName("GetById");
        group.MapPost("", Post);
        group.MapPut("", Put);
        group.MapDelete("", Delete);

        group.MapPut("CancelRequest", CancelRequest);
        group.MapPut("UpdateApproval", UpdateApproval);
    }


    private static async Task<Ok<List<LeaveRequestDto>>> Get(IMediator mediator)
    {
        var data = await mediator.Send(new GetLeaveRequestQuery());
        return TypedResults.Ok(data);
    }
    private static async Task<Results<Ok<LeaveRequestDetailDto>, NotFound>> GetById(Guid ID, IMediator mediator)
    {
        var data = await mediator.Send(new GetLeaveRequestDetailsQuery(ID));
        return TypedResults.Ok(data);
    }
    private static async Task<IResult> Post(IMediator mediator, [FromBody] CreateLeaveRequestCommand command)
    {
        var res = await mediator.Send(command);
        return Results.CreatedAtRoute("GetById", new { id = res });
    }
    private static async Task Put(IMediator mediator, [FromBody] UpdateLeaveRequestCommand command)
    {
        await mediator.Send(command);
    }
    private static async Task Delete(IMediator mediator, [FromBody] DeleteLeaveRequestCommand command)
    {
        await mediator.Send(command);
    }
    private static async Task CancelRequest(IMediator mediator, CancelLeaveRequestCommand command)
    {
        await mediator.Send(command);
    }
    private static async Task UpdateApproval(IMediator mediator, ChangeLeaveRequestApprovalCommand command)
    {
        await mediator.Send(command);
    }
}