using System.Web.Mvc;
using ASPMVC1.Models;

namespace ASPMVC1.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            // Основна дія або перенаправлення на основну сторінку
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            // Перенаправлення на дію аутентифікації
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            // Перенаправлення на дію реєстрації
            return RedirectToAction("Register", "Account");
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            // Логіка для аутентифікації
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            // Логіка для реєстрації
            return RedirectToAction("Login");
        }
    }
}

