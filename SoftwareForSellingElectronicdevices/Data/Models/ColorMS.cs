using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ColorMS
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(60, ErrorMessage = "Color length must be less than or equal to 100 characters")]
        public string NameColorMS { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}