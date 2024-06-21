using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string CodeBill {  get; set; }
        [StringLength(255, ErrorMessage = "Delivery Address must be between 1 and 255 characters.")]
        public string DeliveryAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateOfPayment { get; set; }
        [StringLength(255, ErrorMessage = "Reason Cancellation must be between 1 and 255 characters.")]
        public string? ReasonCancellation{ get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? ShippingMoney { get; set; }

        public string? Description { get; set; }
        public int? TrangThaiGiaoHang { get; set; }
        public int? TrangThaiThanhToan { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<PaymentDetail> PaymentDetails { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; }

    }
}
