using ShopOnline.Application.Domains;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}