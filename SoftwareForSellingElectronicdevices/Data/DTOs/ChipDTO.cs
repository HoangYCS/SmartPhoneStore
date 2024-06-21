using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ChipDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Chip name is required")]
        [StringLength(50, ErrorMessage = "Chip name length must be less than or equal to 50 characters")]
        public string NameChip { get; set; }
    }
}
