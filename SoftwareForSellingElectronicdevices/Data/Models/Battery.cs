using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Battery
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Type is required")]
        [StringLength(150, ErrorMessage = "Type length must be less than or equal to 150 characters")]
        public string BatteryType {  get; set; }
        public int BatteryCapacity { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}