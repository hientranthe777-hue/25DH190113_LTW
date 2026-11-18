using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using _25DH190113_LTW.Models;
using _25DH190113_LTW.Models.ViewModel;

namespace _25DH190113_LTW.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Đăng ký tài khoản Khách hàng (GET)
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // 1. Đăng ký tài khoản Khách hàng (POST) - Lưu vào cả bảng User và Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }

                // Tạo bản ghi trong bảng User với UserRole = "C" (Customer)
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "C"
                };
                db.Users.Add(user);

                // Tạo bản ghi tương ứng trong bảng Customer
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username
                };
                db.Customers.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        // 2. Đăng nhập Khách hàng (GET)
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // 2. Đăng nhập Khách hàng (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // Lưu ý cột Password trong SQL là NCHAR(50) nên dùng .Trim() khi so sánh
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password.Trim() == model.Password && u.UserRole == "C");
                if (user != null)
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Username == user.Username);
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;
                    Session["CustomerName"] = customer != null ? customer.CustomerName : user.Username;

                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            }
            return View(model);
        }

        // 3. Đăng xuất
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}