using Scalar.AspNetCore;
using vT.ApiDomains;

var builder = WebApplication.CreateBuilder(args);

builder.AddDomainService();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Scalar Example API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
    app.MapDefaultEndpoints();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();