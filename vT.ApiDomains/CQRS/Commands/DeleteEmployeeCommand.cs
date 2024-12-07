using MediatR;
using vT.ApiDomains.Data;

namespace vT.ApiDomains.CQRS.Commands;

public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

public class DeleteEmployeeCommandHandler(EmployeeDbContext dbContext) : IRequestHandler<DeleteEmployeeCommand, bool>
{
    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FindAsync(request.Id, cancellationToken);
        if (employee == null) throw new KeyNotFoundException($"Expected record for Id {request.Id} not found.");

        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}