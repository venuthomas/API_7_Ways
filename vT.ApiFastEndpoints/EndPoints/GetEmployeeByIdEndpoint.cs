using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using vT.ApiDomains.CQRS.Queries;
using vT.ApiDomains.Models;
using vT.ApiFastEndpoints.RequestModels;

namespace vT.ApiFastEndpoints.EndPoints;

public class GetEmployeeByIdEndpoint(IMediator mediator) : Endpoint<EmployeeByIdRequest,
    Results<Ok<Employee>,
        NotFound,
        ProblemDetails>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/fastEndpointAPI/GetEmployeeById/{id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<Employee>, NotFound, ProblemDetails>> ExecuteAsync(
        EmployeeByIdRequest req, CancellationToken ct)
    {
        await Task.CompletedTask;

        if (req.id == 0) 
            return TypedResults.NotFound();
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(req.id));
        return TypedResults.Ok(employee);
    }
}