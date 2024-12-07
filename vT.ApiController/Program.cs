using vT.ApiDomains;

var builder = WebApplication.CreateBuilder(args);

builder.AddDomainService();
builder.Services.AddOutputCache(); //Added For Cache
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

app.UseOutputCache(); //Added For Cache

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.UseDeveloperExceptionPage();
    app.MapDefaultEndpoints();
}

app.MapControllers();
app.UseHttpsRedirection();


app.Run();