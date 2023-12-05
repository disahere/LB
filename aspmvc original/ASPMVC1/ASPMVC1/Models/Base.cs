using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ASPMVC1.Models
{
    public class Base : Controller
    {
        private List<UserModel> registeredUsers;
        private string registeredUsersFileName = "saves/json/registeredUsers.json";

        public UserModel UserModel { get; set; }

        public Base()
        {
            UserModel = new UserModel();
            LoadRegisteredUsers();
        }

        private void LoadRegisteredUsers()
        {
            if (System.IO.File.Exists(registeredUsersFileName))
            {
                registeredUsers = JsonConvert.DeserializeObject<List<UserModel>>(System.IO.File.ReadAllText(registeredUsersFileName));
            }
            else
            {
                registeredUsers = new List<UserModel>();
            }
        }

        private void SaveRegisteredUsers()
        {
            System.IO.File.WriteAllText(registeredUsersFileName, JsonConvert.SerializeObject(registeredUsers));
        }

        private bool IsUserRegistered(string username)
        {
            return registeredUsers.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private bool ValidateUser(string username, string password)
        {
            return registeredUsers.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
        }

        private void RegisterUser(string username, string password)
        {
            UserModel.Username = username;
            UserModel.Password = password;

            var newUserModel = new UserModel { Username = UserModel.Username, Password = UserModel.Password };
            registeredUsers.Add(newUserModel);
            SaveRegisteredUsers();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { success = false, message = "Будь ласка, введіть ім'я користувача та пароль." });
            }

            if (IsUserRegistered(username))
            {
                return Json(new { success = false, message = "Користувач з таким ім'ям вже зареєстрований." });
            }

            RegisterUser(username, password);
            return Json(new { success = true, message = "Ви успішно зареєструвалися!" });
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { success = false, message = "Будь ласка, введіть ім'я користувача та пароль." });
            }

            if (ValidateUser(username, password))
            {
                return Json(new { success = true, message = "Ви успішно увійшли в систему!" });
            }
            else
            {
                return Json(new { success = false, message = "Невірне ім'я користувача або пароль." });
            }
        }
    }
}