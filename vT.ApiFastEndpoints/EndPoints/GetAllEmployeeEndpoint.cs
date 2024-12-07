using FastEndpoints;
using MediatR;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;

namespace vT.ApiFastEndpoints.EndPoints;

public class GetAllEmployeeEndpoint(IMediator mediator) : EndpointWithoutRequest<List<Employee>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/fastEndpointAPI/GetAllEmployee");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        await SendAsync(result);
    }
}