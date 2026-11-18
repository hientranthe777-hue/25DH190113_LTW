using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using _25DH190113_LTW.Models;
using _25DH190113_LTW.Models.ViewModel;

namespace _25DH190113_LTW.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra đúng tài khoản có UserRole == "A" (Admin)
                var admin = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password.Trim() == model.Password && u.UserRole == "A");
                if (admin != null)
                {
                    Session["AdminUsername"] = admin.Username;
                    Session["UserRole"] = admin.UserRole;
                    FormsAuthentication.SetAuthCookie(admin.Username, false);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Tài khoản Admin hoặc mật khẩu không chính xác!");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Remove("AdminUsername");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
    }
}