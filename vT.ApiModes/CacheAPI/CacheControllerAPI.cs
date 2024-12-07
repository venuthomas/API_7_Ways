using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using vT.ApiModes.CQRS.Queries;

namespace vT.ApiModes.CacheAPI;

[Route("api/cacheControllerAPI")]
[ApiController]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllEmployees")]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetAllEmployees()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("GetEmployeeById/{id}")]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(employee);
    }
}