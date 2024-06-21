using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public  class ProductAdminViewModel
    {
        public int Id { get; set; }
        public string Url {  get; set; }
        public string PhoneName { get; set; }
        public string Brand { get; set; }
        public string OperatingSystem { get; set; }
        public string Status { get; set; }
        public string Rom {  get; set; }
        public string Ram { get; set; }
        public decimal Price { get; set; }
        public string Battery { get;set; }
        public string Chip { get; set; }
        public string Screen { get; set; }
    }
}
