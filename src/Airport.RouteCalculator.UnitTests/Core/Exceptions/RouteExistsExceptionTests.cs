namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class RouteExistsExceptionTests
    {
        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new RouteExistsException(); };

            //Assert
            act.Should().ThrowExactly<RouteExistsException>()
                        .WithMessage("A rota já foi cadastrada.");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Route exists custom";

            //Act
            var act = () => { throw new RouteExistsException(message); };

            //Assert
            act.Should().ThrowExactly<RouteExistsException>()
                        .WithMessage(message);
        }
    }
}
