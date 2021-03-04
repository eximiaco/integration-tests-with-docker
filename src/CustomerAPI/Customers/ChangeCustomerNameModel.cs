using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Customers
{
    public class ChangeCustomerNameModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
