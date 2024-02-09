using Bislerium_Cafe.Models;
using Bislerium_Cafe.Utils;
using System.Text.Json;

namespace Bislerium_Cafe.Services
{
    public class CustomerServices
    {
        private OrderServices _orderServices;

        public CustomerServices(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        // Retrieves the list of customers.
        public List<MemberCustomer> GetCustomerListFromJsonFile()
        {
            string customersFilePath = AppUtils.GetCustomersListFilePath();

            if (!File.Exists(customersFilePath))
            {
                return new List<MemberCustomer>();
            }

            var json = File.ReadAllText(customersFilePath);

            return JsonSerializer.Deserialize<List<MemberCustomer>>(json);

        }

        // Saves the list of customers.
        public void SaveCustomerListInJsonFile(List<MemberCustomer> customers)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string customerListFilePath = AppUtils.GetCustomersListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(customers);

            File.WriteAllText(customerListFilePath, json);
        }

        // Retrieves a customer by their phone number.
        public MemberCustomer GetCustomerByPhoneNum(string customerPhoneNum)
        {
            List<MemberCustomer> customers = GetCustomerListFromJsonFile();
            MemberCustomer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            return customer;
        }

        // Adds a new customer to the list.
        public void AddCustomer(MemberCustomer _customer)
        {

            MemberCustomer isCustomerExists = GetCustomerByPhoneNum(_customer.CustomerPhoneNum);

            if (isCustomerExists != null)
            {
                throw new Exception("Customer Already exists");
            }

            List<MemberCustomer> customers = GetCustomerListFromJsonFile();
            customers.Add(_customer);

            SaveCustomerListInJsonFile(customers);
        }

        // Updates a customer's order count and saves the updated list.
  
        public void UpdateRedeemedCoffeeCount(string customerPhoneNum, int redeemedCoffeeCount)
        {
            List<MemberCustomer> customers = GetCustomerListFromJsonFile();
            MemberCustomer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            customer.RedeemedCoffeeCount = redeemedCoffeeCount;

            SaveCustomerListInJsonFile(customers);
        }

   
        public bool CheckIfCustomerIsReguralMember(string customerPhoneNum)
        {
            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            //Disount offer for customer
            int month = DateTime.Now.Month - 1;
            int year = month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

            int totalOrderCount = orders
                .Where(order => order.CustomerPhoneNum == customerPhoneNum && order.OrderDateTime.Month == month && order.OrderDateTime.Year == year)
                .GroupBy(order => order.OrderDateTime.Day)
                .ToList().Count();
            //if the day is order is greater than 25 its regular customer
            return totalOrderCount >= 25;
        }

        // This method counts the TotalFreeCoffeeCount.
        public int TotalFreeCoffeeCount(string customerPhoneNum)
        {

            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            int totalOrderCount = orders
                .Where(order => order.CustomerPhoneNum == customerPhoneNum)
                .ToList().Count();

            return totalOrderCount / 10;
        }
    }
}
