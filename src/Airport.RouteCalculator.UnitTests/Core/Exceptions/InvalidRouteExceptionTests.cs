namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class InvalidRouteExceptionTests
    {
        [Fact]
        public void Constructor_ExceptionWithValidationErrors_ShouldThrow()
        {
            //Arrange
            var validationErrorValue = new[] { "'From' must be informed" };
            var validationErrors = new Dictionary<string, string[]>()
            {
                {"From",  validationErrorValue}
            };

            //Act
            var act = () =>
            {
                try
                {
                    throw new InvalidRouteException(validationErrors);
                }
                catch (InvalidRouteException ex)
                {
                    ex.ValidationErrors["From"].Equals(validationErrorValue)
                                               .Should()
                                               .Be(true);
                    throw;
                }
            };

            //Assert
            act.Should().ThrowExactly<InvalidRouteException>()
                        .WithMessage("Rota inválida");
        }

        [Fact]
        public void Constructor_ExceptionWithMessageAndValidationErrors_ShouldThrow()
        {
            //Arrange
            var validationErrorValue = new[] { "'From' must be informed" };
            var validationErrors = new Dictionary<string, string[]>()
            {
                {"From",  validationErrorValue}
            };
            var message = "Invalid route custom";

            //Act
            var act = () =>
            {
                try
                {
                    throw new InvalidRouteException(validationErrors, message);
                }
                catch (InvalidRouteException ex)
                {
                    ex.ValidationErrors["From"].Equals(validationErrorValue)
                                               .Should()
                                               .Be(true);
                    throw;
                }
            };

            //Assert
            act.Should().ThrowExactly<InvalidRouteException>()
                        .WithMessage(message);
        }

        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new InvalidRouteException(); };

            //Assert
            act.Should().ThrowExactly<InvalidRouteException>()
                        .WithMessage("Rota inválida.");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Invalid route custom";

            //Act
            var act = () => { throw new InvalidRouteException(message); };

            //Assert
            act.Should().ThrowExactly<InvalidRouteException>()
                        .WithMessage(message);
        }
    }
}
