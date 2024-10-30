namespace PO_Assignment_Project.Models
{
    public class Payment
    {
        public int Id { get; set; }
       // public string TotalPayment {  get; set; }
        public string? Balance {  get; set; }
        public string? PaymentType { get; set; }
        public int PaymentAmount { get; set; }
        public string? PaymentMethod {  get; set; }
        public Contractor? Contractor { get; set; }
        public DateOnly PaidOn { get; set; }
        public Site? Site { get; set; }
    }
}
