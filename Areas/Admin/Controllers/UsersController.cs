using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using _25DH190113_LTW.Models;
using PagedList;

namespace _25DH190113_LTW.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // Quản lý tài khoản Admin (UserRole == "A"): Xem, Tìm kiếm, Phân trang
        public ActionResult Index(string searchTerm, int? page)
        {
            var admins = db.Users.Where(u => u.UserRole == "A").AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                admins = admins.Where(u => u.Username.Contains(searchTerm));
            }

            admins = admins.OrderBy(u => u.Username);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.SearchTerm = searchTerm;

            return View(admins.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserRole = "A"; // Gán cứng Role Admin
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserRole = "A";
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}