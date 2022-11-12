using Microsoft.AspNetCore.Components.Routing;

namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.ViewModels
{
    public class RouteViewModelFixture
    {
        private readonly char[] _letters;
        private readonly Random _numberGenerator;

        public RouteViewModelFixture()
        {
            _letters = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            _numberGenerator = new Random();
        }

        public RouteViewModel GenerateValid()
        {
            return GenerateValidCollection(1).FirstOrDefault();
        }

        public IEnumerable<RouteViewModel> GenerateValidCollection(int quantity)
        {
            return new Faker<RouteViewModel>().RuleFor(r => r.Id, Guid.NewGuid())
                                              .RuleFor(r => r.Origem, GenerateLocation())
                                              .RuleFor(r => r.Destino, GenerateLocation())
                                              .RuleFor(r => r.Valor, _numberGenerator.Next(50, 100))
                                              .Generate(quantity);
        }

        public RouteViewModel GenerateValidFromEntity(Route product)
        {
            return new Faker<RouteViewModel>().RuleFor(r => r.Id, product.Id)
                                              .RuleFor(r => r.Origem, product.From)
                                              .RuleFor(r => r.Destino, product.To)
                                              .RuleFor(r => r.Valor, product.Value)
                                              .Generate();
        }

        private string GenerateLocation()
        {
            return $"{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}";
        }

        public RouteViewModel GenerateInvalid()
        {
            return new();
        }

        public RouteViewModel GenerateInvalid(InvalidRouteViewModelField invalidField, RouteViewModel routeViewModel)
        {
            switch (invalidField)
            {
                case InvalidRouteViewModelField.FROM:
                    routeViewModel.Origem = string.Empty;
                    break;
                case InvalidRouteViewModelField.FROM_EQUALS_TO:
                    routeViewModel.Origem = routeViewModel.Destino;
                    break;
                case InvalidRouteViewModelField.TO:
                    routeViewModel.Destino = string.Empty;
                    break;
                case InvalidRouteViewModelField.TO_EQUALS_FROM:
                    routeViewModel.Destino = routeViewModel.Origem;
                    break;
                case InvalidRouteViewModelField.VALUE:
                    routeViewModel.Valor = 0;
                    break;
                default:
                    return GenerateInvalid();
            }

            return routeViewModel;
        }
    }

    public enum InvalidRouteViewModelField
    {
        FROM,
        FROM_EQUALS_TO,
        TO,
        TO_EQUALS_FROM,
        VALUE,
        ALL
    }
}
