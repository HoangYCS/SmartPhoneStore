using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Chip
    {
        public int Id {  get; set; }
        [Required(ErrorMessage = "Chip name is required")]
        [StringLength(50, ErrorMessage = "Chip name length must be less than or equal to 50 characters")]
        public string NameChip { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}