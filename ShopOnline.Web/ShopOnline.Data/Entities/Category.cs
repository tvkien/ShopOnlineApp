using ShopOnline.Data.Enums;

namespace ShopOnline.Data.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentID { get; set; }
        public Status Status { get; set; }
    }
}