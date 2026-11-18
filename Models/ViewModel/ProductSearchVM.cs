using System.Collections.Generic;
using PagedList;

namespace _25DH190113_LTW.Models.ViewModel
{
    public class ProductSearchVM
    {
        // Tiêu chí tìm kiếm theo tên, mô tả hoặc loại nhạc cụ
        public string SearchTerm { get; set; }

        // Tiêu chí tìm kiếm theo khoảng giá tối thiểu - tối đa
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        // Thứ tự sắp xếp
        public string SortOrder { get; set; }

        // Thuộc tính hỗ trợ phân trang
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 5;

        // Danh sách sản phẩm đã phân trang
        public IPagedList<Product> Products { get; set; }
    }
}