namespace ShopOnline.Domains
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int CategoryID { get; set; }
        public string LanguageID { get; set; }
    }
}