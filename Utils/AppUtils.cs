namespace Bislerium_Cafe.Utils
{
    internal class AppUtils
    {
        public static string GetDesktopDirectoryPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "users.json");
        }

        public static string GetCofeeListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "coffeeList.json");
        }

        public static string GetAddInItemsListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "addInsList.json");
        }

        public static string GetCustomersListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "customers.json");
        }

        public static string GetOrderItemListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orderItems.json");
        }

        public static string GetOrderListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orders.json");
        }
    }
}
