namespace Bislerium_Cafe.Models
{
    public class OrderCart
    {
        public Guid OrderItemID { get; set; } = Guid.NewGuid();
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

    }
}