namespace Bislerium_Cafe.Models
{
    public class MemberCustomer
    {
        public Guid CustomerID { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNum { get; set; }
        public bool IsRegularMember { get; set; } = false;
        public int RedeemedCoffeeCount { get; set; } = 0;
    }
}
