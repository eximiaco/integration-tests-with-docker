using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CustomerAPI.Customers
{
    public class Name : ValueObject
    {
        protected Name()
        {
        }

        private Name(string name)
        {
            Value = name;
        }

        public string Value { get; private set; }

        public static implicit operator string(Name name) => name.Value;
        public static explicit operator Name(string name) => new Name(name);
        
        public static Result<Name> New(Maybe<string> nameOrNothing)
        {
            return nameOrNothing.ToResult("error")
                .Ensure(x => !string.IsNullOrEmpty(x) && x.Length <= 10, "Name is empty or bigger than 10 characters")
                .Map(x => new Name(x));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
