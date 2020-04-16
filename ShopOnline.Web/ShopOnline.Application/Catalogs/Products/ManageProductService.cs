using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Domains;
using ShopOnline.Data.Entities;
using ShopOnline.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    class ManageProductService : IManageProductService
    {
        private readonly ShopDBContext shopDBContext;

        public ManageProductService(ShopDBContext shopDBContext)
        {
            this.shopDBContext = shopDBContext;
        }

        public async Task AddViewcount(int productId)
        {
            var product = await shopDBContext.Products.FindAsync(productId);
            product.ViewCount += 1;

            await shopDBContext.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };

            shopDBContext.Products.Add(product);
            await shopDBContext.SaveChangesAsync();
            return product.ID;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await shopDBContext.Products.FindAsync(productId);

            if (product == null)
            {
                throw new Exception($"Can not find the product by {productId}");
            }

            shopDBContext.Products.Remove(product);

            return await shopDBContext.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await shopDBContext.Products.FindAsync(request.Id);
            var productTranslations = await shopDBContext.ProductTranslations.FirstOrDefaultAsync(x =>
            x.ProductId == request.Id &&
            x.LanguageId == request.LanguageId);

            if (product == null || productTranslations == null)
            {
                throw new Exception($"Cannot find a product with id: {request.Id}");
            }

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            return await shopDBContext.SaveChangesAsync();
        }
    }
}