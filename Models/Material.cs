using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Material
    {
        public int ID { get; set; }

        [Required]
        public string Code { get; set; }

        [MaxLength(50)]
        public string ShortText { get; set; }

        public string LongText { get; set; }

        [Required]
        public string Unit { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }

        [Range(0, int.MaxValue)]
        public int MinOrderQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}
