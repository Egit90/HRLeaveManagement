using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Models.Email;
using HRLeaveManagement.Infrastructure.EmailService;
using HRLeaveManagement.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        service.AddTransient<IEmailSender, EmailSender>();
        service.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return service;
    }
}
