using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Data;
using vT.ApiDomains.Polly;
using vT.ApiDomains.Validations;

namespace vT.ApiDomains;

public static class Extensions
{
    private const string _sQLConnectionString =
        "Data Source=172.24.32.1;Initial Catalog=EmployeeDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";

    public static IHostApplicationBuilder AddDomainService(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(_sQLConnectionString));
        builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(GetAllEmployeesQueryHandler).Assembly,
                    typeof(GetEmployeeByIdQueryHandler).Assembly,
                    typeof(CreateEmployeeCommandHandler).Assembly,
                    typeof(DeleteEmployeeCommandHandler).Assembly,
                    typeof(UpdateEmployeeCommandHandler).Assembly);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            }
        );

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)); // MediatR Validation behavior
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PollyPipelineBehavior<,>));
        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                context.Database.EnsureCreated();
            }
        return app;
    }
}