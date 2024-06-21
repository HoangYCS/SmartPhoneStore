using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ColorDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Color code is required")]
        public string NameColorMS { get; set; }
    }
}
