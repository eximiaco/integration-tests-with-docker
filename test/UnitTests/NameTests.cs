using CustomerAPI.Customers;
using Xunit;

namespace UnitTests
{
    public class NameTests
    {
        [Fact]
        public void Name_Should_Be_Length_Less_Than_Ten()
        {
            // Arrange
            var value = "01234567890";

            // Act
            var actual = Name.New(value);

            // Assert
            Assert.True(actual.IsFailure);
        }
    }
}
