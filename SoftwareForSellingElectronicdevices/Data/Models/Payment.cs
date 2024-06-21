using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name must be between 1 and 255 characters", MinimumLength = 1)]
        public string Name { get; set; }

    }
}