using PO_Assignment_Project.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class PurchaseOrderDetails
    {
        public int ID { get; set; }

        public int PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public int MaterialID { get; set; }
        public Material Material { get; set; }

        public decimal ItemQuantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public string? ItemNotes { get; set; }
        [DataType(DataType.Date)]
        [DateValidatorHelper(ErrorMessage = "The date must be today or in the future.")]
        public DateTime ExpectedDate { get; set; }
    }
}
