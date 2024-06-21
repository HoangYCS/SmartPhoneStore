using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ROMMemory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ROM memory name is required")]
        [StringLength(100, ErrorMessage = "ROM memory name length must be less than or equal to 100 characters")]
        public string NameROMMemory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}