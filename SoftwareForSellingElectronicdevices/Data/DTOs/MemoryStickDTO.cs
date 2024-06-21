using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MemoryStickDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Memory stick name is required")]
        [StringLength(100, ErrorMessage = "Memory stick name length must be less than or equal to 100 characters")]
        public string NameMemoryStick { get; set; }

    }
}
