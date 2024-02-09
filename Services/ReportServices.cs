using Bislerium_Cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_Cafe.Services
{
    public class ReportServices
    {
        private OrderServices _orderServices { get; set; }

        public ReportServices(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        //Generates the order list monthly or daily
        public List<Order> GenerateOrderList(string reportType, string reportDate)
        {
            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            if (reportType.ToLower() == "monthly")
            {
                orders = orders.Where(x => reportDate == x.OrderDateTime.ToString("MM-yyyy")).ToList();
            }
            else if (reportType.ToLower() == "daily")
            {
                orders = orders.Where(x => reportDate == x.OrderDateTime.ToString("dd-MM-yyyy")).ToList();
            }

            return orders;
        }

        public Dictionary<string, List<OrderCart>> GenerateMostPurchasedCoffeeAndAddInsList(List<Order> orders)
        {
            //Get all order items.
            List<OrderCart> allOrderItems = orders
            .SelectMany(order => order.OrderItems)
            .ToList();

            //Get all coffee and add-ins.
            List<OrderCart> coffeeList = allOrderItems.Where(item => item.ItemType == "coffee").ToList();
            List<OrderCart> addInsList = allOrderItems.Where(item => item.ItemType == "add-in").ToList();

            List<OrderCart> mostOrderedCoffee = coffeeList
            .GroupBy(coffee => coffee.ItemName)
            .Select(group =>
            {
                var itemName = group.Key;
                int totalQuantity = group.Sum(orderItem => orderItem.Quantity);

                return new OrderCart
                {
                    ItemName = itemName,
                    Quantity = totalQuantity,
                };
            }).ToList();

            List<OrderCart> mostOrderedAddInsItem = coffeeList
            .GroupBy(addIn => addIn.ItemName)
            .Select(group =>
            {
                var itemName = group.Key;
                int totalQuantity = group.Sum(orderItem => orderItem.Quantity);

                return new OrderCart
                {
                    ItemName = itemName,
                    Quantity = totalQuantity,
                };
            }).ToList();

            //Return the dictionary.
            return new Dictionary<string, List<OrderCart>>
            {
                { "coffees", mostOrderedCoffee.Take(5).ToList() },
                { "add-ins", mostOrderedAddInsItem.Take(5).ToList() }
            };
        }


    }
}
