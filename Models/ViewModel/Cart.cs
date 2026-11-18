using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace _25DH190113_LTW.Models.ViewModel
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items => items;

        // Gom nhóm các nhạc cụ trong giỏ theo Danh mục
        public List<IGrouping<string, CartItem>> GroupedItems => items.GroupBy(i => i.Category ?? "Nhạc cụ khác").ToList();

        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 6;

        // Danh sách nhạc cụ tương tự cùng danh mục với sản phẩm trong giỏ
        public IPagedList<Product> SimilarProducts { get; set; }

        public void AddItem(int productId, string productImage, string productName, decimal unitPrice, int quantity, string category)
        {
            var existingItem = items.FirstOrDefault(i => i.ProductID == productId);
            if (existingItem == null)
            {
                items.Add(new CartItem
                {
                    ProductID = productId,
                    ProductImage = productImage,
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    Quantity = quantity,
                    Category = category
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            items.RemoveAll(i => i.ProductID == productId);
        }

        public decimal TotalValue()
        {
            return items.Sum(i => i.TotalPrice);
        }

        public void Clear()
        {
            items.Clear();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = items.FirstOrDefault(i => i.ProductID == productId);
            if (item != null)
            {
                if (quantity > 0)
                    item.Quantity = quantity;
                else
                    RemoveItem(productId);
            }
        }
    }
}