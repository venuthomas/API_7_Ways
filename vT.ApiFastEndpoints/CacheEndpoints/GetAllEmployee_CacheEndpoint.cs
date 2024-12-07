using FastEndpoints;
using MediatR;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;

namespace vT.ApiFastEndpoints.CacheEndpoints;

public class GetAllEmployeeCacheEndpoint(IMediator mediator) : EndpointWithoutRequest<List<Employee>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/fastEndpointCacheAPI/GetAllEmployee");
        AllowAnonymous();
        ResponseCache(86400); // Cache for 1 day 
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery(), ct);
        await SendAsync(result, cancellation: ct);
    }
}