using LoginMVC.Data;
using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginMVC.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public Category CreateCategory(string name, int displayOrder)
        {
            var category = new Category()
            {
                Name = name,
                DisplayOrder = displayOrder
            };
            return category;
        }

        public async void Delete(int id)
        {
            var categoty = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(categoty);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category>? GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
