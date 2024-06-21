using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category of the camera is required")]
        [StringLength(100, ErrorMessage = "Category of the camera length must be less than or equal to 100 characters")]
        public string Name { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
    }
}
