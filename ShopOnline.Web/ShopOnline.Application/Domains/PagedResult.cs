using System.Collections.Generic;

namespace ShopOnline.Application.Domains
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}