

namespace Bislerium_Cafe.Models
{
    public class GlobalState
    {
        public RoleUser CurrentUser { get; set; }
        public string AppBarTitle { get; set; }

        public List<OrderCart> OrderItems { get; set; } = new();

    }
}

