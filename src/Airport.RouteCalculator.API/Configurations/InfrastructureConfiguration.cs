namespace Airport.RouteCalculator.API.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<SqlServerContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();

            builder.Services.AddDbContext<SqlServerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
            });

            return builder;
        }
    }
}
