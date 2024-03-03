using HRLeaveManagement.Application;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api;

public static class LeaveAllocationController
{

    public static void MapLeaveAllocationController(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/LeaveAllocation").WithOpenApi();
        group.MapGet("", Get);
        group.MapGet("{id}", GetById).WithName("GetAllocationById");
        group.MapPost("", Post);
        group.MapPut("", Put);
        group.MapDelete("", Delete);
    }


    private static async Task<Ok<List<LeaveAllocationDto>>> Get(IMediator _mediator)
    {
        var data = await _mediator.Send(new GetLeaveTypeAllocationQuery());
        return TypedResults.Ok(data);
    }

    private static async Task<Results<Ok<LeaveAllocationDetailsDto>, NotFound>> GetById(IMediator mediator, Guid id)
    {
        var res = await mediator.Send(new GetLeaveAllocationDetailQuery(id));
        if (res == null) return TypedResults.NotFound();

        return TypedResults.Ok(res);
    }
    private static async Task<IResult> Post(IMediator mediator, CreateLeaveAllocationCommand leaveAllocation)
    {
        var res = await mediator.Send(leaveAllocation);
        return Results.CreatedAtRoute("GetAllocationById", new { id = res });
    }
    private static async Task Put(IMediator mediator, [FromBody] UpdateLeaveAllocationCommand command)
    {
        await mediator.Send(command);
    }
    private static async Task Delete(IMediator mediator, [FromBody] DeleteLeaveAllocationCommand command)
    {
        await mediator.Send(command);
    }

}
