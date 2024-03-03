using HRLeaveManagement.Api;
using HRLeaveManagement.Api.Controllers;
using HRLeaveManagement.Api.Middleware;
using HRLeaveManagement.Application;
using HRLeaveManagement.Infrastructure;
using HRLeaveManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("all", builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapLeaveAllocationController();
app.MapLeaveTypeController();
app.MapLeaveRequestController();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
