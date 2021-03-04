using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAPI.Customers
{
    public class CustomerEntityMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Name, x => x.Property(y => y.Value).HasMaxLength(255).IsRequired());
            builder.Property(x => x.BirthDate).IsRequired();
        }
    }
}
