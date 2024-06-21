
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ROMMemoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ROM memory name is required")]
        [StringLength(100, ErrorMessage = "ROM memory name length must be less than or equal to 100 characters")]
        public string NameROMMemory { get; set; }
    }
}
