using System.Web.Mvc;
using GameStore.WebUI.Infrastructure.Abstract;
using GameStore.WebUI.Models;
using GameStore.Domain.Entities;
using GameStore.Domain.Concrete;
using System.Linq;
using System.Web.Security;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                User user = null;
                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.UserName && u.Password == model.Password);

                }
                if (user != null&& user.Role=="Admin")                    
                {
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}