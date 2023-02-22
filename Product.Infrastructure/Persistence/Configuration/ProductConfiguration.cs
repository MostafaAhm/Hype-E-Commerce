using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.HasOne(b => b.ProductType).WithMany()
                .HasForeignKey(b => b.ProductTypeId);

        }
    }
}
