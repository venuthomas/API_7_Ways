using vT.Api_gPRC.Services;
using vT.ApiDomains;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.AddDomainService();
var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapDefaultEndpoints();
app.MapGrpcService<EmployeeGrpcService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();