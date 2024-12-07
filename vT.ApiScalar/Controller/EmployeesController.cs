using MediatR;
using Microsoft.AspNetCore.Mvc;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.CQRS.Queries;

namespace vT.ApiScalar.Controller;

[Route("api/scalarControllerAPI")]
[ApiController]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllEmployees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("GetEmployeeById/{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(employee);
    }

    [HttpPost("CreateEmployee")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        var employee = await _mediator.Send(createEmployeeCommand);
        return Ok(employee);
    }

    [HttpPut("UpdateEmployeeById/{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand updatedEmployeeCommand)
    {
        if (id != updatedEmployeeCommand.Id) return BadRequest();

        bool updatedSuccessfully = await _mediator.Send(updatedEmployeeCommand);
        return Ok(new { updatedSuccessfully = updatedSuccessfully });
    }

    [HttpDelete("DeleteEmployeeById/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        bool deletedSuccessfully = await _mediator.Send(new DeleteEmployeeCommand(id));
        return Ok(new { deletedSuccessfully = deletedSuccessfully });
    }
}