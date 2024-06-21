using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProductExcelDTO
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Chip { get; set; }
        public string Battery { get; set; }
        public string ChargingPort { get; set; }
        public string MemoryStick { get; set; }
        public string OperatingSystemProduct { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Screen { get; set; }
        public string RAMMemory { get; set; }
        public string ROMMemory { get; set; }
        public string ColorMS { get; set; }
        public string? FrontCamera { get; set; }
        public string BackCamera { get; set; }
        public List<string> ListCategory { get; set; }
        public IEnumerable<string> ListSim { get; set; }
        public int Status { get; set; }
        public List<string> ListUrl { get; set; } = new List<string>();
        public string? Description { get; set; }
    }
}
