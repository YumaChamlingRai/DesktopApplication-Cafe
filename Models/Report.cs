using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_Cafe.Models
{
    public class Report
    {
        public List<Order> Orders { get; set; }
        public string ReportType { get; set; }
        public string ReportDate { get; set; }
        public double TotalRevenue { get; set; } = 0;
        public List<OrderCart> CoffeeList { get; set; }
        public List<OrderCart> AddInsList { get; set; }


    }
}
