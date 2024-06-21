
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.Arm;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name length must be less than or equal to 100 characters")]
        public string ProductName { get; set; }
        [StringLength(100, ErrorMessage = "Front Camera length must be less than or equal to 100 characters")]
        public string? FrontCamera { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Back Camera length must be less than or equal to 100 characters")]
        public string BackCamera { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required(ErrorMessage = "Price name is required")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [ForeignKey("Chip")]
        public int ChipId { get; set; }
        public virtual Chip Chip { get; set; }

        [ForeignKey("Battery")]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        [ForeignKey("ChargingPort")]
        public int ChargingPortId { get; set; }
        public virtual ChargingPort ChargingPort { get; set; }

        [ForeignKey("MemoryStick")]
        public int MemoryStickId { get; set; }
        public virtual MemoryStick MemoryStick { get; set; }

        [ForeignKey("OperatingSystemProduct")]
        public int OperatingSystemProductId { get; set; }
        public virtual OperatingSystemProduct OperatingSystemProduct { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<PhoneSIMType> PhoneSIMTypes { get; set; } = new List<PhoneSIMType>();

        [ForeignKey("Screen")]
        public int ScreenId { get; set; }
        public virtual Screen Screen { get; set; }

        [ForeignKey("RAMMemory")]
        public int RAMMemoryId { get; set; }
        public virtual RAMMemory RAMMemory { get; set; }

        [ForeignKey("ROMMemory")]
        public int ROMMemoryId { get; set; }
        public virtual ROMMemory ROMMemory { get; set; }

        [ForeignKey("ColorMS")]
        public int ColorMSId { get; set; }
        public virtual ColorMS ColorMS { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
        public int Status { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
        public string? Description { get; set; }
    }
}

