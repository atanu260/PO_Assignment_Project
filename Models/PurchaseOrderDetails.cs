namespace PO_Assignment_Project.Models
{
    public class PurchaseOrderDetails
    {
        public long ID { get; set; }

        public long PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public int MaterialID { get; set; }
        public Material Material { get; set; }

        public decimal ItemQuantity { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public string ItemNotes { get; set; }
        public DateTime ExpectedDate { get; set; }
    }
}
