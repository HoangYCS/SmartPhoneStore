using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class OperatingSystemProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Operating system name is required")]
        [StringLength(100, ErrorMessage = "Operating system name length must be less than or equal to 100 characters")]
        public string NameOperatingSystem { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}