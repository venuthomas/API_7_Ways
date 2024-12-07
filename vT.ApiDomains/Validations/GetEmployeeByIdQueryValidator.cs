using FluentValidation;
using vT.ApiDomains.CQRS.Queries;

namespace vT.ApiDomains.Validations;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID must be greater than zero.");
    }
}