using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        [ForeignKey("Bill")]
        public int BillID { get; set; }
        public virtual Bill Bill { get; set; }
        [ForeignKey("Payment")]
        public int PaymentID { get; set; }
        public virtual Payment Payment { get; set; }
    }
}