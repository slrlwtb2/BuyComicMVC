using LoginMVC.Data;
using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LoginMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public CategoryController(ICategoryRepository categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Categories = await _categoryRepository.GetCategoriesAsync();
            return View(Categories);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        public async Task<IActionResult> EditCategory(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetCategory(id);
            ViewBag.Name = category.Name;
            ViewBag.DisplayOrder = category.DisplayOrder;
            return View(category);
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetCategory(id);
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategoryPOST(Category category)
        {
            _context.Categories.Remove(category);
            await _categoryRepository.Save();
            TempData["delete"] = "Delete successfully";
            return RedirectToAction("Index");
        }
        [HttpPost("Category/EditCategory")]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (category.DisplayOrder <= 0 || category.DisplayOrder > 100)
            {
                ModelState.AddModelError("displayOrder", "Display order should be between 1 to 100");
            }
            if (category.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("name", "Name cannot be empty");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                await _categoryRepository.Save();
                TempData["update"] = "Update category successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Name = category.Name;
            ViewBag.DisplayOrder = category.DisplayOrder;
            return View();
        }


        [HttpPost("Category/CreateCategory")]
        public async Task<IActionResult> CreateCategory(string name, int displayOrder)
        {
            if (displayOrder <= 0 || displayOrder > 100)
            {
                ModelState.AddModelError("displayOrder", "Display order should be between 1 to 100");
            }
            if (name.IsNullOrEmpty())
            {
                ModelState.AddModelError("name", "Name cannot be empty");
            }
            if (ModelState.IsValid)
            {
                var category = _categoryRepository.CreateCategory(name, displayOrder);
                await _categoryRepository.AddCategory(category);
                await _categoryRepository.Save();
                TempData["create"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Name = name;
            ViewBag.DisplayOrder = displayOrder;
            return View();
        }
    }
}
