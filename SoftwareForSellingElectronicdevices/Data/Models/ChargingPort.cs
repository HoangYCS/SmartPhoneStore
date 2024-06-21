using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ChargingPort
    {
        public int Id {  get; set; }
        [Required(ErrorMessage = "Charging port name is required")]
        [StringLength(50, ErrorMessage = "Charging port name length must be less than or equal to 50 characters")]
        public string ChargingPortName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}