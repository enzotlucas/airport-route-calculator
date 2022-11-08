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

        private string GenerateLocation()
        {
            return $"{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}";
        }

        public RouteViewModel GenerateInvalid()
        {
            return new();
        }
    }
}
