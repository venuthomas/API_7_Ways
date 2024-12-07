using MediatR;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;
using vT.ApiGraphQL.Models;

namespace vT.ApiGraphQL.Mutations;

public class EmployeeMutations
{
    public async Task<Employee> CreateEmployee(
        [Service] IMediator mediator,
        CreateEmployeeCommand input)
    {
        var employee = await mediator.Send(input);
        return employee;
    }

    [GraphQLName("updateEmployee")]
    public async Task<UpdateResponse> UpdateEmployee(
        [Service] IMediator mediator,
        UpdateEmployeeCommand input)
    {
        var updatedSuccessfully=  await mediator.Send(input);
        return new UpdateResponse { updatedSuccessfully = updatedSuccessfully };
    }
    [GraphQLName("deleteEmployee")]
    public async Task<DeleteResponse> DeleteEmployee(
        [Service] IMediator mediator,
        int id)
    {
       bool deletedSuccessfully = await mediator.Send(new DeleteEmployeeCommand(id));
        return new DeleteResponse { deletedSuccessfully = deletedSuccessfully };
    }
}