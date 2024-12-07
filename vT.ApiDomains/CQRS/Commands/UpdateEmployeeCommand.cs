using MediatR;
using vT.ApiDomains.Data;

namespace vT.ApiDomains.CQRS.Commands;

public record UpdateEmployeeCommand(int Id, string FirstName, string LastName, int Age) : IRequest<bool>;

public class UpdateEmployeeCommandHandler(EmployeeDbContext dbContext) : IRequestHandler<UpdateEmployeeCommand, bool>
{
    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FindAsync(request.Id, cancellationToken);
        if (employee == null) throw new KeyNotFoundException($"Expected record for Id {request.Id} not found.");

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Age = request.Age;
        dbContext.Employees.Update(employee);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}