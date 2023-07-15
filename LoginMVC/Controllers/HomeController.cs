using LoginMVC.Data;
using LoginMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            List<Product> products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id),
                Count = 1,
                ProductId = id,
            };  
        
            return View(shoppingCart);
        }
        [HttpPost("/Home/Details")]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var userId = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.IdentityUserId= userId;
            ShoppingCart cartFromDb = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.IdentityUserId == userId && c.ProductId == shoppingCart.ProductId);
            
            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _context.ShoppingCarts.Update(cartFromDb);
            }
            else
            {
                await _context.ShoppingCarts.AddAsync(shoppingCart);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
