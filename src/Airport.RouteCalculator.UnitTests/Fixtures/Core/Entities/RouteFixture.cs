namespace Airport.RouteCalculator.UnitTests.Fixtures.Core.Entities
{
    public class RouteFixture
    {
        private readonly char[] _letters;
        private readonly Random _numberGenerator;

        public RouteFixture()
        {
            _letters = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            _numberGenerator = new Random();
        }

        public Route GenerateValid()
        {
            return GenerateValidCollection(1).FirstOrDefault();
        }

        public IEnumerable<Route> GenerateValidCollection(int quantity)
        {
            return new Faker<Route>().RuleFor(r => r.Id, Guid.NewGuid())
                                     .RuleFor(r => r.From, GenerateLocation())
                                     .RuleFor(r => r.To, GenerateLocation())
                                     .RuleFor(r => r.Value, _numberGenerator.Next(50, 100))
                                     .RuleFor(r => r.CreatedAt, DateTime.Now)
                                     .RuleFor(r => r.UpdatedAt, DateTime.Now)
                                     .Generate(quantity);
        }

        private string GenerateLocation()
        {
            return $"{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}{_letters[_numberGenerator.Next(0, _letters.Length - 1)]}";
        }

        public Route GenerateInvalid()
        {
            return new();
        }
    }
}
