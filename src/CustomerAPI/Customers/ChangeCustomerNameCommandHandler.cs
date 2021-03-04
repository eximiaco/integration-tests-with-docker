using CSharpFunctionalExtensions;
using CustomerAPI.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Customers
{
    public class ChangeCustomerNameCommandHandler : IRequestHandler<ChangeCustomerNameCommand, Result>
    {
        private readonly CustomerContext _customerContext;
        public ChangeCustomerNameCommandHandler(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        public async Task<Result> Handle(ChangeCustomerNameCommand request, CancellationToken cancellationToken)
        {
            var customer = _customerContext
                .Customers
                .FirstOrDefault(x => x.Id == request.Id);
            if (customer == null)
                return Result.Failure("Customer not found");

            var nameResult = Name.New(request.Name);
            if (nameResult.IsFailure) return nameResult;

            customer.ChangeName(nameResult.Value);
            await _customerContext.SaveChangesAsync();
            return Result.Success();
        }
    }
}
