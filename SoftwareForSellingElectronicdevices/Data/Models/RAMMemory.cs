using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class RAMMemory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RAM memory name is required")]
        [StringLength(100, ErrorMessage = "RAM memory name length must be less than or equal to 100 characters")]
        public string NameRAMMemory { get; set; }

        public virtual ICollection<Product> Products { get; set;}
    }
}