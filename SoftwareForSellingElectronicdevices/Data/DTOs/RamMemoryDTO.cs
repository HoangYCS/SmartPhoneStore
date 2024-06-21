using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class RamMemoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RAM memory name is required")]
        [StringLength(100, ErrorMessage = "RAM memory name length must be less than or equal to 100 characters")]
        public string NameRAMMemory { get; set; }

    }
}
