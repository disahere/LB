using System;
using System.Web.Mvc;
using ASPMVC1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ASPMVC1.Controllers
{
    public class AccountController : Controller
    {
        private string UserDataPath = @"C:\Users\elont\OneDrive\Рабочий стол\LB-main\aspmvc original\ASPMVC1\ASPMVC1\bin\UserData\users.json";

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (ValidateUser(model.Username, model.Password))
            {
                var user = GetUserByUsername(model.Username);
                
                var authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    user.IsAdmin ? "Admin" : "User"
                );
                
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
                return View("Login", model);
            }
        }

        private UserModel GetUserByUsername(string username)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u != null && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        
        private bool ValidateUser(string username, string password)
        {
            var users = LoadUsers();
            
            if (users != null && users.Any(u => u != null && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password))
            {
                return true; 
            }

            return false; 
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            var users = LoadUsers();
            
            users.Add(model);
            
            SaveUsers(users);

            return RedirectToAction("Login");
        }
        
        private void SaveUsers(List<UserModel> users)
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(UserDataPath));
            
            string json = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(UserDataPath, json);
        }


        private List<UserModel> LoadUsers()
        {
            if (System.IO.File.Exists(UserDataPath))
            {
                string json = System.IO.File.ReadAllText(UserDataPath);
                return JsonConvert.DeserializeObject<List<UserModel>>(json);
            }
            else
            {
                SaveUsers(new List<UserModel>());
                return new List<UserModel>();
            }
        }
    }
}