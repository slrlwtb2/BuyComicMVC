using LoginMVC.Models;

namespace LoginMVC.Repository.Interfaces
{
    public interface IShoppingCartRepository
    {
        void Update(ShoppingCart shoppingCart);
        Task Add(ShoppingCart shoppingCart);
    }
}
