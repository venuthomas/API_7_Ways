using MediatR;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;

namespace vT.ApiGraphQL.Queries;

public class EmployeeQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<Employee> GetEmployeeById(
        [Service] IMediator mediator,
        int id)
    {
        return await mediator.Send(new GetEmployeeByIdQuery(id));
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLName("getAllEmployees")]
    public async Task<List<Employee>> GetAllEmployees(
        [Service] IMediator mediator)
    {
        return await mediator.Send(new GetAllEmployeesQuery());
    }
}