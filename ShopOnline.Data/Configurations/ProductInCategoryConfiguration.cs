using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;

namespace ShopOnline.Data.Configurations
{
    class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");

            builder.HasKey(x => new { x.ProductID, x.CategoryID });
            builder.HasOne(x => x.Product)
                   .WithMany(x => x.ProductInCategories)
                   .HasForeignKey(x => x.ProductID);
            builder.HasOne(x => x.Category)
                   .WithMany(x => x.ProductInCategories)
                   .HasForeignKey(x => x.CategoryID);
        }
    }
}