namespace Airport.RouteCalculator.Core.Validators
{
    public class RouteValidator : AbstractValidator<Route>
    {
        public RouteValidator()
        {
            RuleFor(r => r.From).NotNull()
                                .NotEmpty()
                                .NotEqual(r => r.To)
                                .WithName("Origem");

            RuleFor(r => r.To).NotNull()
                              .NotEmpty()
                              .NotEqual(r => r.From)
                              .WithName("Destino");

            RuleFor(r => r.Value).NotNull()
                                 .NotEmpty()
                                 .GreaterThan(0)
                                 .WithName("Valor");
        }
    }
}
