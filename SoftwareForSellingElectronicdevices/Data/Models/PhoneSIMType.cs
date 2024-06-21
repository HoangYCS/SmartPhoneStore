using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Data.Models
{
    public class PhoneSIMType
    {
        [Key]
        public int ID { get; set; } 

        [ForeignKey("Product")]
        public int ProductId { get; set; } 

        [ForeignKey("SIMType")]
        public int SIMTypeID { get; set; } 

        public virtual Product Product { get; set; }
        public virtual SIMType SIMType { get; set; }
    }
}