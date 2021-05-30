using FluentValidation;

namespace WebApi.Controllers.CovidApi
{
    public class RequestValidator : AbstractValidator<CountryNameRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(60);
        }
    }
}