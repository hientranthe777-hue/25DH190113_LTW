using System.Linq;
using System.Net;
using System.Web.Mvc;
using _25DH190113_LTW.Models;
using _25DH190113_LTW.Models.ViewModel;
using PagedList;

namespace _25DH190113_LTW.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // 1. Trang chủ Tiệm Bá Dương: Tự động nạp 20 nhạc cụ + Tìm kiếm + Phân trang
        public ActionResult Index(string searchTerm, int? categoryId, int? page)
        {
            // Tự động nạp 4 danh mục và 20 sản phẩm nhạc cụ vào SQL Server
            ProductRepository.SeedDatabase(db);

            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId.Value);
                ViewBag.CategoryName = db.Categories.Find(categoryId.Value)?.CategoryName;
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p => p.ProductName.Contains(searchTerm)
                                            || p.ProductDecription.Contains(searchTerm)
                                            || p.Category.CategoryName.Contains(searchTerm));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            model.FeaturedProducts = products
                .OrderByDescending(p => p.OrderDetails.Count())
                .ThenBy(p => p.ProductID)
                .Take(10)
                .ToList();

            model.NewProducts = products
                .OrderBy(p => p.ProductID)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Categories = db.Categories.ToList();
            return View(model);
        }

        // 2. Xem Chi tiết nhạc cụ
        public ActionResult ProductDetails(int? id, int? quantity, int? page)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product pro = db.Products.Find(id);
            if (pro == null) return HttpNotFound();

            var products = db.Products
                .Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID)
                .AsQueryable();

            ProductDetailsVM model = new ProductDetailsVM();
            int pageNumber = page ?? 1;
            int pageSize = model.PageSize;

            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToList();
            model.TopProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

            if (quantity.HasValue && quantity.Value > 0)
            {
                model.quantity = quantity.Value;
            }

            return View("ProductDetails", model);
        }

        public ActionResult ProductDetail(int? id, int? quantity, int? page)
        {
            return ProductDetails(id, quantity, page);
        }
    }
}