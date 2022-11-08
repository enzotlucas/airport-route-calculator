namespace Airport.RouteCalculator.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                c.CustomSchemaIds(SchemaIdStrategy);
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return builder;
        }

        private static string SchemaIdStrategy(Type currentClass)
        {
            return currentClass.Name.Replace("ViewModel", string.Empty);
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider,
                                       IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _configuration));
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description,
                                                   IConfiguration configuration)
        {
            var info = new OpenApiInfo()
            {
                Title = configuration.GetValue<string>("Swagger:Title"),
                Version = description.ApiVersion.ToString(),
                Description = configuration.GetValue<string>("Swagger:Description"),
                Contact = new OpenApiContact()
                {
                    Name = configuration.GetValue<string>("Swagger:Contact:Name"),
                    Email = configuration.GetValue<string>("Swagger:Contact:Email")
                },
                License = new OpenApiLicense()
                {
                    Name = configuration.GetValue<string>("Swagger:License:Name"),
                    Url = new Uri(configuration.GetValue<string>("Swagger:License:Url"))
                }
            };

            if (description.IsDeprecated)
                info.Description += " This version is deprecated!";

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];

                foreach (var contentType in response.Content.Keys)
                    if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                        response.Content.Remove(contentType);
            }

            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
