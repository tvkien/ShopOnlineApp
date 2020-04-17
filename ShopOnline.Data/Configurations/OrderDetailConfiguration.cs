using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;

namespace ShopOnline.Data.Configurations
{
    class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => new { x.OrderID, x.ProductID });
            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails);
            builder.HasOne(x => x.Product).WithMany(x => x.OrderDetails);
        }
    }
}