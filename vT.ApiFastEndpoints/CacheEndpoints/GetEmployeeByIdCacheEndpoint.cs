using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;
using vT.ApiFastEndpoints.RequestModels;

namespace vT.ApiFastEndpoints.CacheEndpoints;

public class GetEmployeeByIdCacheEndpoint(IMediator mediator) : Endpoint<EmployeeByIdRequest,
    Results<Ok<Employee>,
        NotFound,
        ProblemDetails>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/fastEndpointCacheAPI/GetEmployeeById/{id}");
        AllowAnonymous();
        ResponseCache(86400); // Cache for 1 day based on ID
    }

    public override async Task<Results<Ok<Employee>, NotFound, ProblemDetails>> ExecuteAsync(
        EmployeeByIdRequest req, CancellationToken ct)
    {
        await Task.CompletedTask; 

        if (req.id == 0) 
            return TypedResults.NotFound();
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(req.id), ct);
        return TypedResults.Ok(employee);
    }
}