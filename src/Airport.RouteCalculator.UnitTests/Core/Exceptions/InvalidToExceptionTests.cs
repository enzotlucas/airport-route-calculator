namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class InvalidToExceptionTests
    {
        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new InvalidToException(); };

            //Assert
            act.Should().ThrowExactly<InvalidToException>()
                        .WithMessage("O destino da rota está inválido");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Invalid to custom";

            //Act
            var act = () => { throw new InvalidToException(message); };

            //Assert
            act.Should().ThrowExactly<InvalidToException>()
                        .WithMessage(message);
        }
    }
}
