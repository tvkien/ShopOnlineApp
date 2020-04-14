namespace ShopOnline.Data.Entities
{
    public class Cart
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Qanlity { get; set; }
        public decimal Price { get; set; }
    }
}