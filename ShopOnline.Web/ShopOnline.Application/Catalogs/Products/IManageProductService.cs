using ShopOnline.Domains;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IManageProductService
    {
        Task<int> CreateAsync(ProductCreateRequest request);

        Task<bool> UpdateAsync(ProductUpdateRequest request);

        Task<bool> DeleteAsync(int productId);

        //Task<bool> UpdatePrice(int productId, decimal newPrice);

        //Task<bool> UpdateStock(int productId, int addedQuantity);

        //Task AddViewcountAsync(int productId);

        //Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        //Task<int> AddImages(int productId, List<IFormFile> files);

        //Task<int> RemoveImages(int imageId);

        //Task<int> UpdateImage(int imageId, string caption, bool isDefault);

        //Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}