using Data.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Data.DTOs
{
    public class CheckOutConfimDTO
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string WardCode { get; set; }
        public string SpecificAddress { get; set; }
        public string? Note { get; set; }
        public int PaymentId { get; set; }
        public List<CartLineVM> CartLines { get; set; } = new List<CartLineVM>();
    }
}
