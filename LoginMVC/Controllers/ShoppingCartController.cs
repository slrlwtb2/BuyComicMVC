using LoginMVC.Data;
using LoginMVC.DTO;
using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LoginMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, ApplicationDbContext context)
        {
            _shoppingCartRepository= shoppingCartRepository;
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
            double totalOrder = 0;
            double totalPrice = 0;
            foreach (var item in cart.ShoppingCartList)
            {
                totalOrder += item.Count;
                totalPrice += item.Product.Price * item.Count;
            }
            cart.TotalOrder = totalOrder;
            cart.TotalPrice = totalPrice;
            return View(cart);
        }
        public async Task<IActionResult> Edit(int productId)
        {

            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var userId = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var shoppingCart = await _context.ShoppingCarts.Where(u => u.IdentityUserId == userId && u.ProductId == productId).FirstOrDefaultAsync();
            return View(shoppingCart);
        }
        [HttpPost("ShoppingCart/Edit")]
        public async Task<IActionResult> Edit(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int productId)
        {

            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var userId = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var shoppingCart = await _context.ShoppingCarts
                .Where(u => u.IdentityUserId == userId && u.ProductId == productId)
                .Include(u => u.Product)
                .FirstOrDefaultAsync();
            return View(shoppingCart);
        }
        [HttpPost("ShoppingCart/Delete")]
        public async Task<IActionResult> Delete(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
