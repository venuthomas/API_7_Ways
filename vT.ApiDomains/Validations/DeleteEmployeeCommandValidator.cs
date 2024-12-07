using FluentValidation;
using vT.ApiDomains.CQRS.Commands;

namespace vT.ApiDomains.Validations;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID must be greater than zero.");
    }
}