
var builder = WebApplication.CreateBuilder(args)
                            .AddApiConfiguration()
                            .AddSwaggerConfiguration()
                            .AddApplicationServices()
                            .AddInfrastructureServices();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseSwaggerConfiguration();

app.Run();