namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class InvalidValueExceptionTests
    {
        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new InvalidValueException(); };

            //Assert
            act.Should().ThrowExactly<InvalidValueException>()
                        .WithMessage("O valor da rota está inválido");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Invalid value custom";

            //Act
            var act = () => { throw new InvalidValueException(message); };

            //Assert
            act.Should().ThrowExactly<InvalidValueException>()
                        .WithMessage(message);
        }
    }
}
