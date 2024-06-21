using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(100, ErrorMessage = "Brand name length must be less than or equal to 100 characters")]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}