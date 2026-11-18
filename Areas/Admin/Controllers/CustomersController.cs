using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using _25DH190113_LTW.Models;
using PagedList;

namespace _25DH190113_LTW.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Xem, tìm kiếm, sắp xếp, phân trang tài khoản Khách hàng
        public ActionResult Index(string searchTerm, string sortOrder, int? page)
        {
            var customers = db.Customers.Include(c => c.User).AsQueryable();

            // Tìm kiếm theo Tên, SĐT, Email hoặc Username
            if (!string.IsNullOrEmpty(searchTerm))
            {
                customers = customers.Where(c => c.CustomerName.Contains(searchTerm)
                                              || c.CustomerPhone.Contains(searchTerm)
                                              || c.CustomerEmail.Contains(searchTerm)
                                              || c.Username.Contains(searchTerm));
            }

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_asc":
                    customers = customers.OrderBy(c => c.CustomerName);
                    break;
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.CustomerName);
                    break;
                default:
                    customers = customers.OrderBy(c => c.CustomerID);
                    break;
            }

            // Phân trang
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortOrder = sortOrder;
            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // 2. Xem danh sách đơn hàng của 1 khách hàng cụ thể
        public ActionResult CustomerOrders(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = db.Customers.Find(id);
            if (customer == null) return HttpNotFound();

            var orders = db.Orders
                .Where(o => o.CustomerID == id.Value)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            ViewBag.Customer = customer;
            return View(orders);
        }

        // 3. Xem chi tiết đơn hàng của khách hàng đó
        public ActionResult OrderDetails(int? orderId)
        {
            if (orderId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = db.Orders
                .Include("Customer")
                .Include("OrderDetails.Product")
                .FirstOrDefault(o => o.OrderID == orderId.Value);

            if (order == null) return HttpNotFound();
            return View(order);
        }
    }
}