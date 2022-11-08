namespace Airport.RouteCalculator.API.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            builder.Services.AddControllers()
                            .AddJsonOptions(x =>
                            {
                                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<LoggerMiddleware>();

            return app;
        }
    }
}
