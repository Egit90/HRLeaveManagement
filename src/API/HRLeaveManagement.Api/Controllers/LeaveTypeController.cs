using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers;
public static class LeaveTypeController
{

    public static void MapLeaveTypeController(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/LeaveTypes").WithOpenApi();
        group.MapGet("", GetLeaveTypes);
        group.MapGet("{id}", GetLeaveTypeDetail).WithName("GetLeaveTypeByID");
        group.MapPost("", CreateLeaveType);
        group.MapPut("", UpdateLeaveType);
        group.MapDelete("", DeleteRecord);

    }

    public static async Task<Ok<List<LeaveTypeDto>>> GetLeaveTypes(IMediator mediator)
    {
        var data = await mediator.Send(new GetLeaveTypeQuery());
        return TypedResults.Ok(data);
    }

    public static async Task<Results<Ok<LeaveTypeDetailDto>, NotFound>> GetLeaveTypeDetail(IMediator mediator, Guid id)
    {
        var res = await mediator.Send(new GetLeaveTypeDetailsQuery(id));

        if (res == null) return TypedResults.NotFound();

        return TypedResults.Ok(res);
    }

    public static async Task<IResult> CreateLeaveType(IMediator mediator, CreateLeaveTypeCommand leaveType)
    {
        var result = await mediator.Send(leaveType);
        return Results.CreatedAtRoute("GetLeaveTypeByID", new { id = result });
    }

    public static async Task<Unit> UpdateLeaveType(IMediator mediator, UpdateLeaveTypeCommand updateLeaveTypeCommand)
    {
        return await mediator.Send(updateLeaveTypeCommand);
    }

    public static async Task<Results<Ok<Unit>, NotFound>> DeleteRecord(IMediator mediator, [FromBody] DeleteLeaveTypeCommand deleteLeaveTypeCommand)
    {
        try
        {
            var res = await mediator.Send(deleteLeaveTypeCommand);
            return TypedResults.Ok(res);
        }
        catch (System.Exception)
        {
            return TypedResults.NotFound();
        }
    }

}