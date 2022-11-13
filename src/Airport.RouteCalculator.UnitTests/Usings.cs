//Libs
global using Xunit;
global using MediatR;
global using Bogus;
global using AutoMapper;
global using FluentAssertions;
global using NSubstitute;
global using NSubstitute.ExceptionExtensions;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Mvc;

//Tests
global using Airport.RouteCalculator.UnitTests.Fixtures.API.Controllers;
global using Airport.RouteCalculator.UnitTests.Fixtures.Core;
global using Airport.RouteCalculator.UnitTests.Fixtures.API;
global using Airport.RouteCalculator.UnitTests.Fixtures.Application.ViewModels;
global using Airport.RouteCalculator.UnitTests.Fixtures.Application.Commands;
global using Airport.RouteCalculator.UnitTests.Fixtures.Core.Entities;
global using Airport.RouteCalculator.UnitTests.Application.Queries;
global using Airport.RouteCalculator.UnitTests.Fixtures.Application;

//API
global using Airport.RouteCalculator.API.Features.V1.Controllers;

//Application
global using Airport.RouteCalculator.Application.Commands.CreateRoute;
global using Airport.RouteCalculator.Application.Commands.DeleteRoute;
global using Airport.RouteCalculator.Application.Commands.UpdateRoute;
global using Airport.RouteCalculator.Application.Queries.GetRouteById;
global using Airport.RouteCalculator.Application.Queries.GetRoutes;
global using Airport.RouteCalculator.Application.ViewModels;
global using Airport.RouteCalculator.Application.Queries.GetBestCostRoute;
global using Airport.RouteCalculator.Application.Services;

//Core
global using Airport.RouteCalculator.Core.Exceptions;
global using Airport.RouteCalculator.Core.Entities;
global using Airport.RouteCalculator.Core.Validators;
global using Airport.RouteCalculator.Core.ValueObjects;
global using Airport.RouteCalculator.Core.DomainObjects;
