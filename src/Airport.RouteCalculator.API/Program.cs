
var builder = WebApplication.CreateBuilder(args)
                            .AddApiConfiguration()
                            .AddSwaggerConfiguration()
                            .AddApplicationServices()
                            .AddCoreConfiguration()
                            .AddInfrastructureServices();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseSwaggerConfiguration();

app.Run();