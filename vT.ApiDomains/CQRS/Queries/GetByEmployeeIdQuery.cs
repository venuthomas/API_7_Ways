using MediatR;
using vT.ApiDomains.Data;
using vT.ApiDomains.Models;

namespace vT.ApiDomains.CQRS.Queries;

public record GetEmployeeByIdQuery(int Id) : IRequest<Employee>;

public class GetEmployeeByIdQueryHandler(EmployeeDbContext dbContext) : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Employees.FindAsync(request.Id, cancellationToken).ConfigureAwait(false)
               ?? throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
    }
}