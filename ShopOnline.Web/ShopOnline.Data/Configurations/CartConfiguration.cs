using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;

namespace ShopOnline.Data.Configurations
{
    class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductID);
        }
    }
}