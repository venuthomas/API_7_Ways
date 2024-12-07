using Carter;
using MediatR;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;

namespace vT.ApiCarter.CarterModules;

public class EmployeeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cartelAPI/GetAllEmployees",
            async (IMediator mediator) => { return await mediator.Send(new GetAllEmployeesQuery()); });


        app.MapGet("/api/cartelAPI/GetEmployeeById/{id}", async (int id, IMediator mediator) =>
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
            return Results.Ok(employee);
        });

        app.MapPost("/api/cartelAPI/CreateEmployee",
            async (CreateEmployeeCommand createEmployeeCommand, IMediator mediator) =>
            {
                var employee = await mediator.Send(createEmployeeCommand);
                return Results.Ok(employee);
            });

        app.MapPut("/api/cartelAPI/UpdateEmployeeById/{id}",
            async (int id, UpdateEmployeeCommand updatedEmployeeCommand, IMediator mediator) =>
            {
                if (id != updatedEmployeeCommand.Id) return Results.BadRequest();

                bool updatedSuccessfully = await mediator.Send(updatedEmployeeCommand);
                return Results.Ok(new { updatedSuccessfully = updatedSuccessfully });
            });

        app.MapDelete("/api/cartelAPI/DeleteEmployeeById/{id}",
            async (int id, IMediator mediator) =>
            {
                var deletedSuccessfully = await mediator.Send(new DeleteEmployeeCommand(id));
                return Results.Ok(new { deletedSuccessfully = deletedSuccessfully });
            });
    }
}