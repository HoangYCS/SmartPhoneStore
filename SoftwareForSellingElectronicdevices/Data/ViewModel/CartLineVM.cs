using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class CartLineVM
    {
        public int CartLineID { get; set; }
        public ProductDetailUserVM ProductDetailUserVM { get; set; }
        public int Quantity { get; set; }
    }
}
