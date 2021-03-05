using CustomerAPI.Customers;
using CustomerAPI.Infrastructure;
using IntegrationTests.Dockable;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    /// <summary>
    /// Teste de integração para a operação de mudança de nome de um Customer.
    /// </summary>
    public class ChangeCustomerNameTests : SqlServerIntegrationTestBase
    {
        public ChangeCustomerNameTests(SqlServerDockerCollectionFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Happy path para a mudança do nome de um customer.
        /// </summary>
        [Fact]
        public async Task Change_Customer_Name_Should_Be_Ok()
        {
            // Setup
            LocalFixture.Seed(this.GetContext);

            // Arrange
            using var context = this.GetContext();
            var handler = new ChangeCustomerNameCommandHandler(context);
            var command = new ChangeCustomerNameCommand(
                new Guid("8e61088d-776d-4c0f-a458-50dccf4e6580"), 
                "José"
            );

            // Act
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(actual.IsSuccess);
            AssertDatabase(command);
        }

        /// <summary>
        /// Happy path para a mudança do nome de um customer.
        /// </summary>
        [Fact]
        public async Task Change_Customer_Maximum_Length_Name_Should_Be_Ok()
        {
            // Setup
            LocalFixture.Seed(this.GetContext);

            // Arrange
            using var context = this.GetContext();
            var handler = new ChangeCustomerNameCommandHandler(context);
            var command = new ChangeCustomerNameCommand(
                new Guid("8e61088d-776d-4c0f-a458-50dccf4e6580"),
                "Mauricio A"
            );

            // Act
            var actual = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(actual.IsSuccess);
            AssertDatabase(command);
        }

        private void AssertDatabase(ChangeCustomerNameCommand command)
        {
            using var assertContext = this.GetContext();
            var customer = assertContext.Customers.FirstOrDefault(x => x.Id == command.Id);
            Assert.NotNull(customer);
            Assert.Equal(command.Name, customer.Name);
        }

        private class LocalFixture
        {
            public static void Seed(Func<CustomerContext> factory)
            {
                using var context = factory();

                var costumer = new Customer(
                    new Guid("8e61088d-776d-4c0f-a458-50dccf4e6580"),
                    (Name)"Maximus",
                    new DateTime(2020, 01, 01)
                );

                context.Customers.Add(costumer);
                context.SaveChanges();
            }
        }
    }
}
