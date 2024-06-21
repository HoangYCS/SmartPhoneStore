using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ChargingPortDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Charging port name is required")]
        [StringLength(50, ErrorMessage = "Charging port name length must be less than or equal to 50 characters")]
        public string ChargingPortName { get; set; }
    }
}
