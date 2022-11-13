namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class InfrastructureExceptionTests
    {
        [Fact]
        public void Constructor_DefaultConstructor_ShouldThrow()
        {
            //Arrange
            var message = "Something is wrong";
            var innerExceptionMessage = "Generic exception message";

            //Act
            var act = () =>
            {
                try
                {
                    throw new Exception(innerExceptionMessage);
                }
                catch (Exception ex)
                {
                    ex.Message.Should().Be(innerExceptionMessage);
                    throw new InfrastructureException(message, ex);
                }
            };

            //Assert
            act.Should().ThrowExactly<InfrastructureException>()
                        .WithMessage(message)
                        .WithInnerException<Exception>();
        }
    }
}
