using System.Linq;
using System.Web;

namespace _25DH190113_LTW.Models.ViewModel
{
    public class CartService
    {
        private readonly HttpSessionStateBase session;

        public CartService(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public Cart GetCart()
        {
            var cart = session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                session["Cart"] = cart;
            }
            session["CartCount"] = cart.Items.Sum(i => i.Quantity);
            return cart;
        }

        public void ClearCart()
        {
            session["Cart"] = null;
            session["CartCount"] = 0;
        }
    }
}