using LoginMVC.Data;
using LoginMVC.DTO;
using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var userId = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            CartDTO cart = new CartDTO()
            {
                ShoppingCartList = await _context.ShoppingCarts.Where(u => u.IdentityUserId == userId).Include(u => u.Product).ToListAsync()
            };
            int total = 0;
            foreach (var item in cart.ShoppingCartList)
            {
                total += item.Count;
            }
            cart.TotalOrder = total;
            return View(cart);
        }
    }
}
