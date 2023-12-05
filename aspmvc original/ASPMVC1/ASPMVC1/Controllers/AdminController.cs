using System.Web.Mvc;
using System.Collections.Generic;
using ASPMVC1.Models;
using Newtonsoft.Json;
using NLog;

namespace ASPMVC1.Controllers
{
    public class AdminController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private List<UserModel> GetAllUsers()
        {
            string userDataPath = @"C:\Users\elont\OneDrive\Рабочий стол\LB-main\aspmvc original\ASPMVC1\ASPMVC1\bin\UserData\users.json";
            
            if (System.IO.File.Exists(userDataPath))
            {
                string json = System.IO.File.ReadAllText(userDataPath);
                List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(json);

                return users;
            }
            
            return new List<UserModel>();
        }
        
        [Authorize]
        public ActionResult UserList()
        {
            var user = System.Web.HttpContext.Current.User;
            
            var role = "Unknown";
            if (user.Identity.IsAuthenticated)
            {
                role = user.IsInRole("Admin") ? "Admin" : "User";
            }
            
            Logger.Info($"User '{user.Identity.Name}' has the role: {role}");

            var users = GetAllUsers();
            return View(users);
        }

        [Authorize]
        public ActionResult DeleteUser(string username)
        {
            return RedirectToAction("UserList");
        }
    }
}