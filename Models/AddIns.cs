namespace Bislerium_Cafe.Models
{
    public class AddIns
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

