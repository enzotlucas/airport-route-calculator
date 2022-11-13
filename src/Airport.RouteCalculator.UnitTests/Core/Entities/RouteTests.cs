namespace Airport.RouteCalculator.UnitTests.Core.Entities
{
    [Collection(nameof(CoreFixtureCollection))]
    public class RouteTests
    {
        private readonly CoreFixture _fixture;

        public RouteTests(CoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_ValidInformations_ShouldReturnRoute()
        {
            //Arrange
            var from = "ABC";
            var to = "DEF";
            var value = 75.00M;
            var validator = new RouteValidator();

            //Act
            var route = new Route(from, to, value, validator);

            //Assert
            route.Should().NotBeNull();
            route.Id.Should().NotBe(Guid.Empty);
            route.From.Should().Be(from);
            route.To.Should().Be(to);
            route.Value.Should().Be(value);
            route.IsValid.Should().Be(true);
            route.CreatedAt.Should().NotBe(DateTime.MinValue);
            route.UpdatedAt.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public void Constructor_InvalidFrom_ShouldThrow()
        {
            //Arrange
            var firstFrom = string.Empty;
            string secondFrom = null;
            var thirdFrom = "AB";
            var to = "DEF";
            var value = 75.00M;
            var validator = new RouteValidator();

            //Act
            var firstAct = () =>
            {
                _ = new Route(firstFrom, to, value, validator);
            };
            var secondtAct = () =>
            {
                _ = new Route(secondFrom, to, value, validator);
            };
            var thirdAct = () =>
            {
                _ = new Route(thirdFrom, to, value, validator);
            };

            //Assert
            firstAct.Should().ThrowExactly<InvalidRouteException>()
                .WithMessage("Rota inválida");

            secondtAct.Should().ThrowExactly<InvalidRouteException>()
                .WithMessage("Rota inválida");

            secondtAct.Should().ThrowExactly<InvalidRouteException>()
               .WithMessage("Rota inválida");
        }

        [Fact]
        public void Constructor_InvalidTo_ShouldThrow()
        {
            //Arrange
            var from = "ABC";
            var firstTo = string.Empty;
            string secondTo = null;
            var thirdTo = "DE";
            var value = 75.00M;
            var validator = new RouteValidator();

            //Act
            var firstAct = () =>
            {
                _ = new Route(from, firstTo, value, validator);
            };
            var secondtAct = () =>
            {
                _ = new Route(from, secondTo, value, validator);
            };
            var thirdAct = () =>
            {
                _ = new Route(from, thirdTo, value, validator);
            };

            //Assert
            firstAct.Should().ThrowExactly<InvalidRouteException>()
                .WithMessage("Rota inválida");

            secondtAct.Should().ThrowExactly<InvalidRouteException>()
                .WithMessage("Rota inválida");

            secondtAct.Should().ThrowExactly<InvalidRouteException>()
               .WithMessage("Rota inválida");
        }

        [Fact]
        public void Constructor_InvalidValue_ShouldThrow()
        {
            //Arrange
            var from = "ABC";
            var to = "DEF";
            var value = 0.0M;
            var validator = new RouteValidator();

            //Act
            var firstAct = () =>
            {
                _ = new Route(from, to, value, validator);
            };

            //Assert
            firstAct.Should().ThrowExactly<InvalidRouteException>()
                .WithMessage("Rota inválida");
        }

        [Fact]
        public void Update_ValidInformation_ShouldUpdateSucessfully()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = "AAA";
            var to = "BBB";
            var value = (decimal)new Random().Next(200, 250);

            //Act
            route.Update(from, to, value);

            //Assert
            route.From.Should().Be(from);
            route.To.Should().Be(to);
            route.Value.Should().Be(value);
            route.IsValid.Should().Be(true);
        }

        [Fact]
        public void Update_ValidFrom_ShouldUpdateFromSucessfully()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = "AAA";
            var to = route.To;
            var value = route.Value;

            //Act
            route.Update(from, to, value);

            //Assert
            route.From.Should().Be(from);
            route.To.Should().Be(to);
            route.Value.Should().Be(value);
            route.IsValid.Should().Be(true);
        }

        [Fact]
        public void Update_ValidTo_ShouldUpdateToSucessfully()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = route.From;
            var to = "BBB";
            var value = route.Value;

            //Act
            route.Update(from, to, value);

            //Assert
            route.From.Should().Be(from);
            route.To.Should().Be(to);
            route.Value.Should().Be(value);
            route.IsValid.Should().Be(true);
        }

        [Fact]
        public void Update_ValidValue_ShouldUpdateValueSucessfully()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = route.From;
            var to = route.To;
            var value = route.Value - 1.0M;

            //Act
            route.Update(from, to, value);

            //Assert
            route.From.Should().Be(from);
            route.To.Should().Be(to);
            route.Value.Should().Be(value);
            route.IsValid.Should().Be(true);
        }

        [Fact]
        public void Update_InvalidFrom_ShoudThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var firstFrom = route.To;
            var secondFrom = "";
            var thirdFrom = "AB";
            var forthFrom = "ABCD";
            var to = route.To;
            var value = route.Value;

            //Act
            var firstAct = () => route.Update(firstFrom, to, value);
            var secondAct = () => route.Update(secondFrom, to, value);
            var thirdAct = () => route.Update(thirdFrom, to, value);
            var forthAct = () => route.Update(forthFrom, to, value);

            //Assert
            firstAct.Should().ThrowExactly<InvalidFromException>();
            secondAct.Should().ThrowExactly<InvalidFromException>();
            thirdAct.Should().ThrowExactly<InvalidFromException>();
            forthAct.Should().ThrowExactly<InvalidFromException>();
        }

        [Fact]
        public void Update_InvalidTo_ShoudThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = route.From;
            var firstTo = route.From;
            var secondTo = "";
            var thirdTo = "AB";
            var forthTo = "ABCD";
            var value = route.Value;

            //Act
            var firstAct = () => route.Update(from, firstTo, value);
            var secondAct = () => route.Update(from, secondTo, value);
            var thirdAct = () => route.Update(from, thirdTo, value);
            var forthAct = () => route.Update(from, forthTo, value);

            //Assert
            firstAct.Should().ThrowExactly<InvalidToException>();
            secondAct.Should().ThrowExactly<InvalidToException>();
            thirdAct.Should().ThrowExactly<InvalidToException>();
            forthAct.Should().ThrowExactly<InvalidToException>();
        }

        [Fact]
        public void Update_InvalidValue_ShoudThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var from = route.From;
            var to = route.To;
            var value = 0.0M;

            //Act
            var act = () => route.Update(from, to, value);

            //Assert
            act.Should().ThrowExactly<InvalidValueException>();
        }
    }
}
