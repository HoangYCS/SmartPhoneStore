using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ScreenDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Screen name is required")]
        [StringLength(100, ErrorMessage = "Screen name length must be less than or equal to 100 characters")]
        public string NameScreen { get; set; }
    }
}
