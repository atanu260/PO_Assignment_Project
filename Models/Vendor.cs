using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Vendor
    {
        public long ID { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(255)]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        public string AddressLine2 { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [StringLength(10)]
        [Phone]
        public string ContactNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValidTillDate { get; set; }

        public bool IsActive { get; set; }
    }
}
