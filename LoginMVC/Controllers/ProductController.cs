using LoginMVC.Data;
using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoginMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository productRepository,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _productRepository = productRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            // var products = await _productRepository.GetProducts();
            var products = await _context.Products.Include(p => p.Category).ToListAsync();


            return View(products);
        }
        public IActionResult CreateProduct()
        {
            List<SelectListItem> categoriesList = _context.Categories.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            ViewBag.Categories = categoriesList;
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            List<SelectListItem> categoriesList = _context.Categories.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            ViewBag.Categories = categoriesList;
            return View(product);
        }
        [HttpPost("/Product/DeleteProduct")]
        public IActionResult DeleteProduct(Product product)
        {
            _productRepository.Delete(product);
            _productRepository.Save();
            TempData["delete"] = "Delete Product success";
            return RedirectToAction("Index");
        }
        [HttpPost("/Product/CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Product");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.ImageURL = "/Images/Product/" + uniqueFileName;
            }

            await _productRepository.AddProduct(product);
            _productRepository.Save();
            TempData["create"] = "Create Product success";
            return RedirectToAction("Index");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            List<SelectListItem> categoriesList = _context.Categories.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
            ViewBag.Categories = categoriesList;
            return View(product);
        }
        [HttpPost("/Product/EditProduct")]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Product");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.ImageURL = "/Images/Product/" + uniqueFileName;
            }
            _productRepository.Update(product);
            _productRepository.Save();
            TempData["update"] = "Update Product success";
            return RedirectToAction("Index");
        }
    }
}
