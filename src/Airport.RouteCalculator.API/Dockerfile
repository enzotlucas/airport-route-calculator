FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Airport.RouteCalculator.API/Airport.RouteCalculator.API.csproj", "Airport.RouteCalculator.API/"]
COPY ["Airport.RouteCalculator.Infrastructure/Airport.RouteCalculator.Infrastructure.csproj", "Airport.RouteCalculator.Infrastructure/"]
COPY ["Airport.RouteCalculator.Core/Airport.RouteCalculator.Core.csproj", "Airport.RouteCalculator.Core/"]
COPY ["Airport.RouteCalculator.Application/Airport.RouteCalculator.Application.csproj", "Airport.RouteCalculator.Application/"]
RUN dotnet restore "Airport.RouteCalculator.API/Airport.RouteCalculator.API.csproj"
COPY . .
WORKDIR "/src/Airport.RouteCalculator.API"
RUN dotnet build "Airport.RouteCalculator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Airport.RouteCalculator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Airport.RouteCalculator.API.dll"]