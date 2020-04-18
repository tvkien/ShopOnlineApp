using Microsoft.AspNetCore.Http;

namespace ShopOnline.BackendApi.Models
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double OriginalPrice { get; set; }
        public int Stock { get; set; }
        public string SeoAlias { get; set; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public IFormFile ThumbnailImage { get; set; }
    }
}