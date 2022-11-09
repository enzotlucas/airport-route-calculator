namespace Airport.RouteCalculator.Core.Validators
{
    public class RouteValidator : AbstractValidator<Route>
    {
        public RouteValidator()
        {
            RuleFor(r => r.From).NotNull()
                                .NotEmpty()
                                .NotEqual(r => r.To)
                                .Length(3)
                                .WithName("Origem");

            RuleFor(r => r.To).NotNull()
                              .NotEmpty()
                              .NotEqual(r => r.From)
                              .Length(3)
                              .WithName("Destino");

            RuleFor(r => r.Value).NotNull()
                                 .NotEmpty()
                                 .GreaterThan(0)
                                 .WithName("Valor");
        }
    }
}
