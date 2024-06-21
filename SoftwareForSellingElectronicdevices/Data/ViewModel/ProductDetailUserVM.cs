using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ProductDetailUserVM
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> Urls { get; set; }
        public string Ram {  get; set; }
        public string Rom { get; set; }
        public string Screen {  get; set; }
        public string Battery { get; set; }
        public string Brand {  get; set; }
        public string Tags { get; set; }
        public string ChargingPort { get; set; }
        public string Chip { get; set; }
        public string MemoryStick { get; set; }
        public string OperatingSystem { get; set; }
        public string Sims { get; set; }
        public string RearCamera { get; set; }
        public string FrontCamera { get; set; }
        public decimal Weight { get; set; }

    }
}
