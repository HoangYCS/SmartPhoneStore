using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MemoryStick
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Memory stick name is required")]
        [StringLength(100, ErrorMessage = "Memory stick name length must be less than or equal to 100 characters")]
        public string NameMemoryStick { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}