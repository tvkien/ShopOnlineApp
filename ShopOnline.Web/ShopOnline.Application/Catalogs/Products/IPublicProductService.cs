using ShopOnline.Application.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}