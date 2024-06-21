using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Data.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name length must be less than or equal to 100 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Chip name is required")]
        public int ChipId { get; set; }
        [Required(ErrorMessage = "Batery name is required")]
        public int BatteryId { get; set; }
        [Required(ErrorMessage = "Charging Port name is required")]
        public int ChargingPortId { get; set; }
        [Required(ErrorMessage = "Memory Stick is required")]
        public int MemoryStickId { get; set; }
        [Required(ErrorMessage = "Operating System is required")]
        public int OperatingSystemProductId { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; }


        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price name is required")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Screen is required")]
        public int ScreenId { get; set; }
        [Required(ErrorMessage = "RAM Memory is required")]
        public int RAMMemoryId { get; set; }
        [Required(ErrorMessage = "ROM Memory is required")]
        public int ROMMemoryId { get; set; }
       
        [Required(ErrorMessage = "Color is required")]
        public int ColorMSId { get; set; }
        [StringLength(100, ErrorMessage = "Product name length must be less than or equal to 100 characters")]
        public string? FrontCamera { get; set; }
        [Required(ErrorMessage = "BackCamera name is required")]
        [StringLength(100, ErrorMessage = "Product name length must be less than or equal to 100 characters")]
        public string BackCamera { get; set; }
        public IEnumerable<int> ListIdCategory { get; set; }
        public IEnumerable<int> ListIdSim {  get; set; }
        public int Status { get; set; }
        public List<string> ListUrl { get; set; } = new List<string>();
        public string? Description { get; set; }
        public int? indexMainImage { get; set; }
    }
}
