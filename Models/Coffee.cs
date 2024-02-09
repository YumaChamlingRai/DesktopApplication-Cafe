namespace Bislerium_Cafe.Models
{
    public class Coffee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CoffeeType { get; set; }
        public double Price { get; set; }

    }
}

