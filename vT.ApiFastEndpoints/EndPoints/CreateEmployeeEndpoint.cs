using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiDomains.Models;

namespace vT.ApiFastEndpoints.EndPoints;

public class CreateEmployeeEndpoint(IMediator mediator) : Endpoint<CreateEmployeeCommand, Employee>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/fastEndpointAPI/CreateEmployee");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateEmployeeCommand req, CancellationToken ct)
    {
        await Task.CompletedTask; 
        var employee = await _mediator.Send(req, ct);
        await SendAsync(employee);
    }
}