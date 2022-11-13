namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class InvalidFromExceptionTests
    {
        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new InvalidFromException(); };

            //Assert
            act.Should().ThrowExactly<InvalidFromException>()
                        .WithMessage("A origem da rota está inválida");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Invalid from custom";

            //Act
            var act = () => { throw new InvalidFromException(message); };

            //Assert
            act.Should().ThrowExactly<InvalidFromException>()
                        .WithMessage(message);
        }
    }
}
