using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginMVC.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Range(1,100,ErrorMessage ="Plaease enter a value between 1 to 100")]
        public int Count { get; set; }

        [ForeignKey("Product")]
        [ValidateNever]
        public int ProductId { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public Product Product { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
