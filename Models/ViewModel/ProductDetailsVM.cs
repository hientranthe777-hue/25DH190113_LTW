using System.Collections.Generic;
using PagedList;

namespace _25DH190113_LTW.Models.ViewModel
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public int quantity { get; set; } = 1;

        // Tính giá trị tạm tính = số lượng * đơn giá
        public decimal estimatedValue => product != null ? quantity * product.ProductPrice : 0;

        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;

        // Danh sách 8 nhạc cụ cùng danh mục
        public List<Product> RelatedProducts { get; set; }

        // Danh sách 8 nhạc cụ bán chạy nhất cùng danh mục (có phân trang)
        public IPagedList<Product> TopProducts { get; set; }
    }
}