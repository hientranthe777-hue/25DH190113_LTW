using System;
using System.Linq;
using System.Web.Mvc;
using _25DH190113_LTW.Models;
using _25DH190113_LTW.Models.ViewModel;

namespace _25DH190113_LTW.Controllers
{
    public class OrderController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        private Cart GetCart()
        {
            return new CartService(Session).GetCart();
        }

        // 1. Trang Thanh toán đặt hàng (GET)
        [HttpGet]
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            string username = Session["Username"] != null ? Session["Username"].ToString() : User.Identity.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var customer = db.Customers.FirstOrDefault(c => c.Username == username);
            if (customer == null) return RedirectToAction("Login", "Account");

            var model = new CheckoutVM
            {
                CartItems = cart.Items.ToList(),
                TotalAmount = cart.TotalValue(),
                OrderDate = DateTime.Now,
                ShippingAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                Username = customer.Username,
                ShippingMethod = "Giao hàng nhanh",
                PaymentMethod = "Tiền mặt"
            };

            return View(model);
        }

        // 2. Xử lý Lưu đơn hàng (POST - Trang 45 bài giảng)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutVM model)
        {
            var cart = GetCart();
            if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Home");

            string username = Session["Username"] != null ? Session["Username"].ToString() : User.Identity.Name;
            var customer = db.Customers.FirstOrDefault(c => c.Username == username);
            if (customer == null) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                string paymentStatus = "Chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
                    case "Chuyển khoản": paymentStatus = "Chờ xác nhận chuyển khoản"; break;
                    case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
                    default: paymentStatus = "Chưa thanh toán"; break;
                }

                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = DateTime.Now,
                    TotalAmount = cart.TotalValue(),
                    PaymentStatus = paymentStatus,
                    PaymentMethod = model.PaymentMethod,
                    ShippingMethod = model.ShippingMethod,
                    DeliveryMethod = model.ShippingMethod,
                    ShippingAddress = model.ShippingAddress,
                    AddressDelivery = model.ShippingAddress,
                    OrderDetails = cart.Items.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();

                new CartService(Session).ClearCart();
                return RedirectToAction("OrderSuccess", new { id = order.OrderID });
            }

            model.CartItems = cart.Items.ToList();
            model.TotalAmount = cart.TotalValue();
            return View(model);
        }

        // 3. Trang Xác nhận đơn hàng (Trang 48 bài giảng)
        public ActionResult OrderSuccess(int id)
        {
            var order = db.Orders.Include("OrderDetails.Product").SingleOrDefault(o => o.OrderID == id);
            if (order == null) return HttpNotFound();
            return View(order);
        }

        // 4. Lịch sử mua hàng / Đơn hàng của tôi (Hỗ trợ cả tên OrderHistory và MyOrder)
        public ActionResult OrderHistory()
        {
            string username = Session["Username"] != null ? Session["Username"].ToString() : User.Identity.Name;
            if (string.IsNullOrEmpty(username)) return RedirectToAction("Login", "Account");

            var customer = db.Customers.FirstOrDefault(c => c.Username == username);
            if (customer == null) return RedirectToAction("Login", "Account");

            var orders = db.Orders
                .Include("OrderDetails.Product")
                .Where(o => o.CustomerID == customer.CustomerID)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public ActionResult MyOrder()
        {
            return RedirectToAction("OrderHistory");
        }
    }
}