namespace PO_Assignment_Project.Models
{
    public class PurchaseOrder
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public long VendorID { get; set; }
        public Vendor Vendor { get; set; }

        public string Notes { get; set; }
        public decimal OrderValue { get; set; }
        public string OrderStatus { get; set; }

        public List<PurchaseOrderDetails> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetails>();
    }
}
