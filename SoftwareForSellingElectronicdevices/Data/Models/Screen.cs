using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Screen
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Screen name is required")]
        [StringLength(100, ErrorMessage = "Screen name length must be less than or equal to 100 characters")]
        public string NameScreen { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}