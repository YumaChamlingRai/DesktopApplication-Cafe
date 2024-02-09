using Bislerium_Cafe.Models;

namespace Bislerium_Cafe.Services
{
    public class OrderCartServices
    {

        // Adds an item to the list.
        public void AddItemInOrderItemsList(List<OrderCart> _orderItems, Guid itemID, string itemName, String itemType, Double itemPrice)
        {

            OrderCart orderItem = _orderItems.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (orderItem != null)
            {
                orderItem.Quantity++;
                orderItem.TotalPrice = orderItem.Quantity * itemPrice;

                return;
            }

            orderItem = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = itemPrice,
                TotalPrice = itemPrice
            };

            _orderItems.Add(orderItem);

        }

        // Deletes an item.
        public void DeleteItemInOrderItemsList(List<OrderCart> _orderItems, Guid orderItemID)
        {
            OrderCart orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }

        // Manages the quantity.
        public void ManageQuantityOfOrderItem(List<OrderCart> _orderItems, Guid orderItemID, string action)
        {
            OrderCart orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                if (action == "add")
                {
                    orderItem.Quantity++;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                else if (action == "subtract" && orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
            }
        }

        // Calculates the total amount.
        public double CalculateTotalAmount(IEnumerable<OrderCart> Elements)
        {
            double totalAmount = 0;

            foreach (var item in Elements)
            {
                totalAmount += item.TotalPrice;
            }
            return totalAmount;
        }

        // Redeems free coffees based on the totalFreeCoffeeCount.
        public Dictionary<string, double> RedeemFreeCoffees(int totalFreeCoffeeCount, List<OrderCart> cartOrderItems)
        {
        
            int totalRedeemedCoffeeCount = 0;
            double totalDiscountAmount = 0;

            if (cartOrderItems.Count == 0 || totalFreeCoffeeCount <= 0)
            {
                return new Dictionary<string, double>();
            }

            int totalItemsQuantityInCart = cartOrderItems
                                                         .Where(item => item.ItemType == "coffee")
                                                          .Sum(item => item.Quantity);

            var coffeeItems = cartOrderItems
                .Where(item => item.ItemType == "coffee")
                .OrderByDescending(item => item.Price)
                .ToList();

            foreach (var orderItem in coffeeItems)
            {
    
                int diffBetweenCartQuantityAndFreeCoffeeCount = Math.Max(0, orderItem.Quantity - totalFreeCoffeeCount);

                int reedeemedItemQuantity = diffBetweenCartQuantityAndFreeCoffeeCount == 0 ? orderItem.Quantity : diffBetweenCartQuantityAndFreeCoffeeCount;
            
                totalRedeemedCoffeeCount += reedeemedItemQuantity;
        
                totalDiscountAmount += (orderItem.Price * reedeemedItemQuantity);

                totalFreeCoffeeCount -= reedeemedItemQuantity;

                if (totalFreeCoffeeCount <= 0)
                {
                    break;
                }
            }

            return new Dictionary<string, double>
            {
                { "discount", totalDiscountAmount },
                { "redeemedCoffeeCount", totalRedeemedCoffeeCount }
            };
        }

    }


}
