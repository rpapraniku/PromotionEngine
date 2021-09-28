namespace DataAccess.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
        public int Price { get; set; }
    }
}
