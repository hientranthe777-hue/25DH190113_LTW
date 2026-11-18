using System.Collections.Generic;
using PagedList;

namespace _25DH190113_LTW.Models.ViewModel
{
    public class HomeProductVM
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 6;

        // Danh sách top 10 nhạc cụ nổi bật / bán chạy nhất
        public List<Product> FeaturedProducts { get; set; }

        // Danh sách nhạc cụ mới có phân trang
        public IPagedList<Product> NewProducts { get; set; }
    }
}