using MediatR;
using Microsoft.EntityFrameworkCore;
using vT.ApiDomains.Data;
using vT.ApiDomains.Models;

namespace vT.ApiDomains.CQRS.Queries;

public record GetAllEmployeesQuery : IRequest<List<Employee>>;

public class GetAllEmployeesQueryHandler(EmployeeDbContext context)
    : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
{
    public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await context.Employees.ToListAsync(cancellationToken);
    }
}