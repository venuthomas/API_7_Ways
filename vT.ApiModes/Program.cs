using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vT.ApiModes.CQRS.Commands;
using vT.ApiModes.CQRS.Queries;
using vT.ApiModes.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseInMemoryDatabase("EmployeeDB"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "EmployeeInstance";
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.UseDeveloperExceptionPage();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
        context.Database.EnsureCreated(); // This creates the database and runs OnModelCreating
        // Alternatively, use context.Database.Migrate(); for applying migrations
    }
}

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseHttpsRedirection();
app.UseOutputCache();
app.UseRouting();



app.MapGet("/api/CacheMinimalAPI/GetAllEmployees", async (IMediator mediator) => await mediator.Send(new GetAllEmployeesQuery())).CacheOutput();

app.MapGet("/api/CacheMinimalAPI/employees/{id}", async (int id, IMediator mediator) =>
{
    var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
    return Results.Ok(employee);
}).CacheOutput();

app.Run();