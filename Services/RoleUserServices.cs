using Bislerium_Cafe.Utils;
using Bislerium_Cafe.Models;
using System.Text.Json;

namespace Bislerium_Cafe.Services
{
    public class RoleUserServices
    {
        // List of default users.
        private readonly List<RoleUser> _seedUsersList = new()
        {
            new RoleUser()
            {
                UserName = "administrator",
                Password = "administrator",
                Role = Role.BisleriumAdmin,
            },
        };

        // Saves a list of users.
        public void SaveAllUsersInJsonFile(List<RoleUser> users)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(users);

            File.WriteAllText(appUsersFilePath, json);
        }

        // Retrieves a list of users.
        public List<RoleUser> GetAllUsersFromJsonFile()
        {
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!File.Exists(appUsersFilePath))
            {
                return new List<RoleUser>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<RoleUser>>(json);
        }

        public void SeedUsers()
        {
            var users = GetAllUsersFromJsonFile();
            if (users.Count == 0)
            {
                SaveAllUsersInJsonFile(_seedUsersList);
            }
        }
        // Authenticates a user during login.
        public RoleUser LogIn(string userName, string password, string role)
        {
            const string errorMessage = "Invalid username or password";

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password is required");
            }

            List<RoleUser> users = GetAllUsersFromJsonFile();

            RoleUser user = users.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.Role.ToString() == role);

            return user ?? throw new Exception(errorMessage);
        }

        // Updates a user's password.
        public RoleUser ChangePassword(RoleUser currentUser, string newPassword, string currentPassword)
        {


            List<RoleUser> users = GetAllUsersFromJsonFile();

            RoleUser user = users.FirstOrDefault(u => u.UserName == currentUser.UserName && u.Role.ToString() == currentUser.Role.ToString());

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool isCurrentPasswordValid = user.Password == currentPassword;

            if (!isCurrentPasswordValid)
            {
                throw new Exception("Incorrect Current password");
            };


            user.Password = newPassword;
            user.HasInitialPasswordChanged = true;

            SaveAllUsersInJsonFile(users);

            return user;
        }
    }
}