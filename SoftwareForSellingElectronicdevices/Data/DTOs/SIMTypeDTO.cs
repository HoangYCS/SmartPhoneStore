using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class SIMTypeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "SIM type is required")]
        [StringLength(50, ErrorMessage = "SIM type length must be less than or equal to 50 characters")]
        public string Type { get; set; }
    }
}
