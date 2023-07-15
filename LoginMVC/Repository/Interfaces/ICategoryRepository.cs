using LoginMVC.Models;

namespace LoginMVC.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Category CreateCategory(string name, int displayOrder);
        Task<Category> GetCategory(int id);
        Task Save();
        void Update(Category obj);
        void Delete(int id);
    }
}
