using LoginMVC.Models;

namespace LoginMVC.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        void Update(Product product);
        void Save();
        void Delete(Product product);
        Task AddProduct(Product product);

        Task<Product> GetById(int id);
    }
}
