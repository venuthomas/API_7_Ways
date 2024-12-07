using FluentValidation;
using vT.ApiDomains.CQRS.Commands;

namespace vT.ApiDomains.Validations;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Employee First Name is required.")
            .Length(2, 50).WithMessage("Employee First Name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Employee Last Name is required.")
            .Length(2, 50).WithMessage("Employee Last Name must be between 2 and 50 characters.");

        RuleFor(x => x.Age)
            .GreaterThan(18).WithMessage("Age must be greater than 18.")
            .LessThan(100).WithMessage("Age must be less than 100.");
    }
}