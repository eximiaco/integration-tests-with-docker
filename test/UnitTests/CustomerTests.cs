using CustomerAPI.Customers;
using CustomerAPI.Seedwork;
using System;
using Xunit;

namespace UnitTests
{
    public class CustomerTests
    {
        [Fact]
        public void Change_Customer_Name_Should_Be_Ok()
        {
            // Arrange
            var expected = (Name)"after";
            var customer = new Customer((Name)"before", new DateTime(1950, 01, 01));

            // Act
            customer.ChangeName(expected);

            // Assert
            Assert.Equal(expected, customer.Name);
        }

        [Fact]
        
        public void Change_Customer_With_Null_Name_Throws()
        {
            // Arrange
            var customer = new Customer((Name)"before", new DateTime(1950, 01, 01));

            // Act, Assert
            Assert.Throws<ContractException>(() => customer.ChangeName(null));
        }
    }
}
