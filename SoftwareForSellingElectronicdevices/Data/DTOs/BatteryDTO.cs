﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class BatteryDTO
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Type is required")]
        [StringLength(150, ErrorMessage = "Type length must be less than or equal to 150 characters")]
        public string BatteryType { get; set; }
        public int BatteryCapacity { get; set; }
    }
}
