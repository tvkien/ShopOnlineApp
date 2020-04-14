using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;

namespace ShopOnline.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.OriginalPrice).IsRequired();
            builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Product);
        }
    }
}