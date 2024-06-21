using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMainImage { get; set; } = false;

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
