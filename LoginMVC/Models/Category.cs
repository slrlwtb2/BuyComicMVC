using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
