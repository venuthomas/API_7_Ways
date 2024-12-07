using Carter;
using vT.ApiDomains;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();
builder.AddDomainService();
builder.Services.AddOpenApi();
builder.Services.AddCarter();
var app = builder.Build();

app.UseOutputCache();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.MapDefaultEndpoints();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();