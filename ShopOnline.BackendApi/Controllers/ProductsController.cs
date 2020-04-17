using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using System.Threading.Tasks;
using ShopOnline.BackendApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ShopOnline.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService publicProductService;
        private readonly IManageProductService manageProductService;
        private readonly IMapper mapper;

        public ProductsController(
            IPublicProductService publicProductService,
            IManageProductService manageProductService,
            IMapper mapper)
        {
            this.publicProductService = publicProductService;
            this.manageProductService = manageProductService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await publicProductService.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainRequest = mapper.Map<Domains.ProductCreateRequest>(request);
            await manageProductService.CreateAsync(domainRequest);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm]ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainRequest = mapper.Map<Domains.ProductUpdateRequest>(request);
            var result = await manageProductService.UpdateAsync(domainRequest);
            
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            var result = await manageProductService.DeleteAsync(productId);

            if (result)
            {
                return Ok();
            }

            return NotFound($"Can not find the Product by {productId}");
        }
    }
}