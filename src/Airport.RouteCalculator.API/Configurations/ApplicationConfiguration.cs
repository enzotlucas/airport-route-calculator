namespace Airport.RouteCalculator.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(CreateRouteCommand));

            builder.Services.AddAutoMapper(typeof(RouteProfile));

            return builder;
        }
    }
}
