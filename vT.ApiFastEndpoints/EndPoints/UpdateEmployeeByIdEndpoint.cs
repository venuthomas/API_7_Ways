using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using vT.ApiDomains.CQRS.Commands;

namespace vT.ApiFastEndpoints.EndPoints;

public class UpdateEmployeeByIdEndpoint(IMediator mediator) : Endpoint<UpdateEmployeeCommand, object>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/api/fastEndpointAPI/UpdateEmployeeById/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEmployeeCommand req, CancellationToken ct)
    {
        bool updatedSuccessfully = await _mediator.Send(req);

        await SendAsync(new {  updatedSuccessfully = updatedSuccessfully}, cancellation: ct);
    }
}