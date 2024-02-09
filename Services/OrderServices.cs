using Bislerium_Cafe.Models;
using Bislerium_Cafe.Utils;
using System.Text.Json;

namespace Bislerium_Cafe.Services
{
    public class OrderServices
    {
        public List<Order> GetOrdersFromJsonFile()
        {
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!File.Exists(orderListFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(orderListFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        public void PlaceOrder(Order order)
        {
            List<Order> orders = GetOrdersFromJsonFile();
            orders.Add(order);

            // Folder path where all the files related are stored.
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(orderListFilePath, json);
        }
    }
}
