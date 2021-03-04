using CSharpFunctionalExtensions;
using CustomerAPI.Seedwork;
using MediatR;
using System;

namespace CustomerAPI.Customers
{
    public class ChangeCustomerNameCommand : Command, IRequest<Result>
    {
        public ChangeCustomerNameCommand(Guid id, string name)
        {
            Preconditions.Requires(id != Guid.Empty);
            Preconditions.Requires(!string.IsNullOrEmpty(name));

            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
