using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using vT.ApiDomains.CQRS.Queries;

namespace vT.ApiController.CacheController;

[Route("api/CacheControllerAPI")]
[ApiController]
public class EmployeesCacheController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllEmployees")]
    [OutputCache(Duration = 86400)] // Cache for 1 day
    public async Task<IActionResult> GetAllEmployees()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("GetEmployeeById/{id}")]
    [OutputCache(Duration = 86400)] // Cache for 1 day based on ID
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(employee);
    }
}