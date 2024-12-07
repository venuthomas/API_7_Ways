using vT.ApiDomains;
using vT.ApiGraphQL.Mutations;
using vT.ApiGraphQL.Queries;
using vT.ApiGraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<EmployeeQueries>()
    .AddMutationType<EmployeeMutations>()
    .AddType<EmployeeType>()
    .AddType<DeletedType>()
    .AddType<UpdatedType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
builder.AddDomainService();

var app = builder.Build();


app.UseRouting();
app.MapGraphQL("/api/graphql");
if (app.Environment.IsDevelopment()) app.MapDefaultEndpoints();


app.Run();