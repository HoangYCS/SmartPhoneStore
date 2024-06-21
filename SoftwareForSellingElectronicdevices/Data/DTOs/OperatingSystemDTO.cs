using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OperatingSystemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Operating system name is required")]
        [StringLength(100, ErrorMessage = "Operating system name length must be less than or equal to 100 characters")]
        public string NameOperatingSystem { get; set; }
    }
}
