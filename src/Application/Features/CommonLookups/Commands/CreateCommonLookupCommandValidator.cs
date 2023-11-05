namespace Application.Features.CommonLookups.Commands;
public class CreateCommonLookupCommandValidator : AbstractValidator<CreateCommonLookupCommand>
{
    public CreateCommonLookupCommandValidator()
    {
        RuleFor(v => v.Code)
            .MaximumLength(10)
            .MinimumLength(4)
            .NotEmpty()
            .NotNull()
            .WithMessage("{0} is required");

        RuleFor(v => v.Name)
            .MaximumLength(10)
            .MinimumLength(4)
            .NotEmpty()
            .NotNull()
            .WithMessage("{0} is required");

        RuleFor(v => v.TypeCode)
            .MaximumLength(10)
            .MinimumLength(4)
            .NotEmpty()
            .NotNull()
            .WithMessage("{0} is required");
    }
}
