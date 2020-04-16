using ShopOnline.Application.Domains;
using ShopOnline.Data.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShopOnline.Application.Catalogs.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly ShopDBContext shopDBContext;

        public PublicProductService(ShopDBContext shopDBContext)
        {
            this.shopDBContext = shopDBContext;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            var query = from p in shopDBContext.Products
                        join pt in shopDBContext.ProductTranslations on p.ID equals pt.ProductId
                        join pic in shopDBContext.ProductInCategories on p.ID equals pic.ProductID
                        join c in shopDBContext.Categories on pic.CategoryID equals c.ID
                        where pt.LanguageId == request.LanguageID  && pic.CategoryID == request.CategoryID
                        select new { p, pt, pic };

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.ID,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = await query.CountAsync(),
                Items = data
            };

            return pagedResult;
        }
    }
}