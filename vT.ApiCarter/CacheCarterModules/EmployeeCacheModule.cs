using Carter;
using MediatR;
using vT.ApiDomains.CQRS.Queries;

namespace vT.ApiCarter.CacheCarterModules;

public class EmployeeCacheModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/CacheCartelAPI/GetAllEmployees",
                async (IMediator mediator) => await mediator.Send(new GetAllEmployeesQuery()))
            .CacheOutput(options => options.Expire(TimeSpan.FromSeconds(86400))); // Cache for 1 day

        app.MapGet("/api/CacheCartelAPI/employees/{id}", async (int id, IMediator mediator) =>
            {
                var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
                return Results.Ok(employee);
            })
            .CacheOutput(options =>
                options.Expire(TimeSpan.FromSeconds(86400)).SetVaryByRouteValue("id")); // Cache for 1 day based on ID
    }
}