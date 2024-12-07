using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using vT.ApiDomains.CQRS.Commands;
using vT.ApiFastEndpoints.RequestModels;

namespace vT.ApiFastEndpoints.EndPoints;

public class DeleteEmployeeEndpoint(IMediator mediator) : Endpoint<EmployeeByIdRequest, object>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("/api/fastEndpointAPI/DeleteEmployeeById/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmployeeByIdRequest req, CancellationToken ct)
    {
       var deletedSuccessfully = await _mediator.Send(new DeleteEmployeeCommand(req.id));
       await SendAsync(new { deletedSuccessfully = deletedSuccessfully }, cancellation: ct);
    }
}