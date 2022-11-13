using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class BusinessExceptionTests
    {
        [Fact]
        public void Constructor_ExceptionWithMessageAndValidationErrors_ShouldThrow()
        {
            //Arrange
            var validationErrorValue = new[] { "'Field' must be informed" };
            var validationErrors = new Dictionary<string, string[]>()
            {
                {"Field",  validationErrorValue}
            };
            var message = "Invalid entity";

            //Act
            var act = () =>
            {
                try
                {
                    throw new BusinessException(validationErrors, message);
                }
                catch (BusinessException ex)
                {
                    ex.ValidationErrors["Field"].Equals(validationErrorValue)
                                                .Should()
                                                .Be(true);
                    throw;
                }
            };

            //Assert
            act.Should().ThrowExactly<BusinessException>()
                        .WithMessage(message);
        }

        [Fact]
        public void Constructor_ExceptionWithMessage_ShouldThrow()
        {
            //Arrange
            var message = "Invalid entity";

            //Act
            var act = () => { throw new BusinessException(message); };

            //Assert
            act.Should().ThrowExactly<BusinessException>()
                        .WithMessage(message);
        }
    }
}
