using MediatR;
using vT.ApiDomains;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();
builder.AddDomainService();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseOutputCache();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.MapDefaultEndpoints();
}

app.UseHttpsRedirection();

// Minimal API endpoints
app.MapGet("/api/minimalAPI/GetAllEmployees",
    async (IMediator mediator) => { return await mediator.Send(new GetAllEmployeesQuery()); });

app.MapGet("/api/minimalAPI/GetEmployeeById/{id}", async (int id, IMediator mediator) =>
{
    var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
    return Results.Ok(employee);
});

app.MapPost("/api/minimalAPI/CreateEmployee",
    async (CreateEmployeeCommand createEmployeeCommand, IMediator mediator) =>
    {
        var employee = await mediator.Send(createEmployeeCommand);
        return Results.Ok(employee);
    });

app.MapPut("/api/minimalAPI/UpdateEmployeeById/{id}",
    async (int id, UpdateEmployeeCommand updatedEmployeeCommand, IMediator mediator) =>
    {
        if (id != updatedEmployeeCommand.Id) return Results.BadRequest();

        bool updatedSuccessfully = await mediator.Send(updatedEmployeeCommand);
        return Results.Ok(new { updatedSuccessfully = updatedSuccessfully });
    });

app.MapDelete("/api/minimalAPI/DeleteEmployeeById/{id}",
    async (int id, IMediator mediator) =>
    {
        bool deletedSuccessfully = await mediator.Send(new DeleteEmployeeCommand(id));
        return Results.Ok(new { deletedSuccessfully = deletedSuccessfully });
    });

app.MapGet("/api/CacheMinimalAPI/GetAllEmployees",
        async (IMediator mediator) => { return await mediator.Send(new GetAllEmployeesQuery()); })
    .CacheOutput(options => options.Expire(TimeSpan.FromSeconds(86400))); // Cache for 1 day

app.MapGet("/api/CacheMinimalAPI/GetEmployeeById/{id}", async (int id, IMediator mediator) =>
    {
        var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
        return Results.Ok(employee);
    })
    .CacheOutput(options =>
        options.Expire(TimeSpan.FromSeconds(86400)).SetVaryByRouteValue("id")); // Cache for 1 day based on ID
app.Run();