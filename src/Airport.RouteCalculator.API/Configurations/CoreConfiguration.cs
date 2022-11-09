namespace Airport.RouteCalculator.API.Configurations
{
    public static class CoreConfiguration
    {
        public static WebApplicationBuilder AddCoreConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<RouteValidator>();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            return builder;
        }
    }
}
