using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Commons;
using ShopOnline.Domains;
using ShopOnline.Data.Entities;
using ShopOnline.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly ShopDBContext shopDBContext;
        private readonly IFileHandlers fileHandlers;

        public ManageProductService(ShopDBContext shopDBContext, IFileHandlers fileHandlers)
        {
            this.shopDBContext = shopDBContext;
            this.fileHandlers = fileHandlers;
        }

        public async Task<int> CreateAsync(ProductCreateRequest request)
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

            if (request.ThumbnailImage != null)
            {
                var imagePath = await fileHandlers.SaveFileAsync(request.ThumbnailImage);

                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = imagePath,
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            shopDBContext.Products.Add(product);
            await shopDBContext.SaveChangesAsync();
            return product.ID;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var product = await shopDBContext.Products.FindAsync(productId);

            if(product == null)
            {
                return false;
            }

            var images = shopDBContext.ProductImages.Where(x => x.ProductId == productId);
            
            foreach (var image in images)
            {
                await fileHandlers.DeleteFileAsync(image.ImagePath);
            }

            shopDBContext.Products.Remove(product);

            return await shopDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ProductUpdateRequest request)
        {
            var product = await shopDBContext.Products.FindAsync(request.Id);
            var productTranslations = await shopDBContext.ProductTranslations.FirstOrDefaultAsync(x =>
            x.ProductId == request.Id &&
            x.LanguageId == request.LanguageId);

            if (product == null || productTranslations == null)
            {
                return false;
            }

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            if (request.ThumbnailImage == null)
            {
                return await shopDBContext.SaveChangesAsync() > 0;
            }

            var thumbnailImage = await shopDBContext.ProductImages
                .FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);

            if (thumbnailImage != null)
            {
                var imagePath = await fileHandlers.SaveFileAsync(request.ThumbnailImage);

                thumbnailImage.FileSize = request.ThumbnailImage.Length;
                thumbnailImage.ImagePath = imagePath;

                shopDBContext.ProductImages.Update(thumbnailImage);
            }

            return await shopDBContext.SaveChangesAsync() > 0;
        }
    }
}