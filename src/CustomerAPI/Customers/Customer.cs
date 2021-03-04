using CustomerAPI.Seedwork;
using System;

namespace CustomerAPI.Customers
{
    public class Customer
    {
        protected Customer()
        {
        }

        public Customer(Name name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public Customer(Guid id, Name name, DateTime birthDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
        }

        public Guid Id { get; private set; }

        public Name Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public void ChangeName(Name name)
        {
            Preconditions.Requires(name != null);
            Name = name;
        }
    }
}
