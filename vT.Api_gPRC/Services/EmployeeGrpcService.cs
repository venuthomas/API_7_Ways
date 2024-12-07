using Grpc.Core;
using MediatR;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;

namespace vT.Api_gPRC.Services;

public class EmployeeGrpcService(IMediator mediator)
    : EmployeeService.EmployeeServiceBase
{
    public override async Task<GetAllEmployeesResponse> GetAllEmployees(
        GetAllEmployeesRequest request,
        ServerCallContext context)
    {
        var employees = await mediator.Send(new GetAllEmployeesQuery());

        var response = new GetAllEmployeesResponse();
        response.Employees.AddRange(employees.Select(e => new EmployeeDto
        {
            Id = e.ID,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Age = e.Age
        }));

        return response;
    }

    public override async Task<GetEmployeeByIdResponse> GetEmployeeById(
        GetEmployeeByIdRequest request,
        ServerCallContext context)
    {
        var employee = await mediator.Send(new GetEmployeeByIdQuery(request.Id));

        return new GetEmployeeByIdResponse
        {
            Employee = new EmployeeDto
            {
                Id = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age
            }
        };
    }

    public override async Task<CreateEmployeeResponse> CreateEmployee(
        CreateEmployeeRequest request,
        ServerCallContext context)
    {
        var command = new CreateEmployeeCommand(
            request.FirstName,
            request.LastName,
            request.Age
        );
    
        var employee = await mediator.Send(command);
    
        return new CreateEmployeeResponse
        {
            Employee = new EmployeeDto
            {
                Id = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age
            }
        };
    }

    public override async Task<UpdateEmployeeResponse> UpdateEmployee(
        UpdateEmployeeRequest request,
        ServerCallContext context)
    {
        var command = new UpdateEmployeeCommand(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Age
        );

        var updatedSuccessfully = await mediator.Send(command);

        return new UpdateEmployeeResponse
        {
            UpdatedSuccessfully = updatedSuccessfully
        };
    }

    public override async Task<DeleteEmployeeResponse> DeleteEmployee(
        DeleteEmployeeRequest request,
        ServerCallContext context)
    {
       var deletedSuccessfully = await mediator.Send(new DeleteEmployeeCommand(request.Id));
        return new DeleteEmployeeResponse
        {
            DeletedSuccessfully = deletedSuccessfully
        };
    }
}