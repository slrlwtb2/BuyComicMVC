using LoginMVC.Models;

namespace LoginMVC.DTO
{
    public class CartDTO
    {
        public List<ShoppingCart> ShoppingCartList { get; set; }
        public double TotalOrder { get; set; }
        public double TotalPrice { get; set; }
    }
}
