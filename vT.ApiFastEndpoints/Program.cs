using FastEndpoints;
using vT.ApiDomains;

var builder = WebApplication.CreateBuilder(args);

builder.AddDomainService();

builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints()
    .AddResponseCaching();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.UseDeveloperExceptionPage();
    app.MapDefaultEndpoints();
}

app.UseResponseCaching() 
    .UseFastEndpoints();
app.UseHttpsRedirection();

app.Run();