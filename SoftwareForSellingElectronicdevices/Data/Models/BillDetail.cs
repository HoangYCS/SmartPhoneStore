using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class BillDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public virtual Bill  Bill { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

    }
}