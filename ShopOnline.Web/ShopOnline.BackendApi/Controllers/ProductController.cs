using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using System.Threading.Tasks;

namespace ShopOnline.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService publicProductService;

        public ProductController(IPublicProductService publicProductService)
        {
            this.publicProductService = publicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await publicProductService.GetAll();
            return Ok(products);
        }
    }
}