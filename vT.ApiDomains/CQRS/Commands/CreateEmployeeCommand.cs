using MediatR;
using vT.ApiDomains.Data;
using vT.ApiDomains.Models;

namespace vT.ApiDomains.CQRS.Commands;

public record CreateEmployeeCommand(string FirstName, string LastName, int Age) : IRequest<Employee>;

public class CreateEmployeeCommandHandler(EmployeeDbContext dbContext) : IRequestHandler<CreateEmployeeCommand, Employee>
{
    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee { FirstName = request.FirstName, LastName = request.LastName, Age = request.Age };
        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync(cancellationToken);
        return employee;
    }
}