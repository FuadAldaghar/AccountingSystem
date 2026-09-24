namespace AccountingSystem.Models
{
    public class PurchaseInvoice
    {
        public int PurchaseInvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public int AccountId { get; set; }

        public Account? Account { get; set; } = null!;

        public ICollection<PurchaseInvoiceDetail> Details { get; set; }
            = new List<PurchaseInvoiceDetail>();
    }
}