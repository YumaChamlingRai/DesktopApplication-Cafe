using Bislerium_Cafe.Models;
using Bislerium_Cafe.Utils;
using System.Text.Json;

namespace Bislerium_Cafe.Services
{
    public class AddInsServices
    {
        // Creating a list of AddIns 
        private readonly List<AddIns> _addInItemsList = new()
        {
            new() { Name = "Caramel Drizzle", Price = 25.0 },
            new() { Name = "Extra Sugar", Price = 15.0 },
            new() { Name = "Whipped Cream", Price = 25.0 },
            new() { Name = "Chocolate Syrup", Price = 16.0 },
            new() { Name = "Vanilla Extract", Price = 28.0 },
            new() { Name = "Cinnamon Powder", Price = 37.0 },
            new() { Name = "Hazelnut Flavor", Price = 45.0 },
            new() { Name = "Almond Milk", Price = 45.0 },
            new() { Name = "Whiskey Shot", Price = 20.0 },
           
        };

        // Adds a new AddInItem
        public void AddAddInItem(String name, double price)

        {
            AddIns addInItem = new()
            {
                Name = name,
                Price = price
            };

            List<AddIns> addInItemList = GetAddInItemsListListFromJsonFile();

            addInItemList.Add(addInItem);

            SaveAddInItemsListInJsonFile(addInItemList);

        }


        // Saves the AddInItems
        public void SaveAddInItemsListInJsonFile(List<AddIns> addInItemList)
        {
            // Folder path where all the files related to app are stored
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string addInItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            // Ensure the directory exists or create it if not
            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(addInItemList);

            File.WriteAllText(addInItemsListListFilePath, json);
        }

        // It Retrieves the AddInItems list 
        public List<AddIns> GetAddInItemsListListFromJsonFile()
        {
            string addInsItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            if (!File.Exists(addInsItemsListListFilePath))
            {
                return new List<AddIns>();
            }

            var json = File.ReadAllText(addInsItemsListListFilePath);

            return JsonSerializer.Deserialize<List<AddIns>>(json);
        }

        // Seed the Add-In items list with predefined items if there is not any Add-In items in the JSON file
        public void SeedAddInItemsList()
        {
            List<AddIns> addInsList = GetAddInItemsListListFromJsonFile();

            if (addInsList.Count == 0)
            {
                SaveAddInItemsListInJsonFile(_addInItemsList);
            }
        }

        //It  Retrieves an AddInItem by its ID) from the list.
        public AddIns GetAddInItemDetailsByID(String addInItemID)
        {
            List<AddIns> addInItemList = GetAddInItemsListListFromJsonFile();
            AddIns addInItem = addInItemList.FirstOrDefault(addIn => addIn.Id.ToString() == addInItemID);
            return addInItem;
        }

        // Updates the xisting Add In items
        public void UpdateAddInItemDetails(AddIns addInItem)
        {
            List<AddIns> addInItemsList = GetAddInItemsListListFromJsonFile();

            AddIns addInItemToUpdate = addInItemsList.FirstOrDefault(_addInItem => _addInItem.Id.ToString() == addInItem.Id.ToString());

          
            if (addInItemToUpdate == null)
            {
                throw new Exception("Add-In item not found");
            }

            addInItemToUpdate.Name = addInItem.Name;
            addInItemToUpdate.Price = Math.Round(addInItem.Price, 2);

            SaveAddInItemsListInJsonFile(addInItemsList);
        }

        // Deletes the AddItem
        public List<AddIns> DeletAddInItem(Guid addInItemID)
        {
            List<AddIns> addInItemsList = GetAddInItemsListListFromJsonFile();
            AddIns addInItem = addInItemsList.FirstOrDefault(item => item.Id.ToString() == addInItemID.ToString());

            if (addInItem != null)
            {
                addInItemsList.Remove(addInItem);
                SaveAddInItemsListInJsonFile(addInItemsList);
            }

            return addInItemsList;
        }
    }
}
