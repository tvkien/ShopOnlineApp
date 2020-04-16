namespace ShopOnline.Application.Domains
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int CategoryID { get; set; }
        public string LanguageID { get; set; }
    }
}