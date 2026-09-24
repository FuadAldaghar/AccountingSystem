namespace AccountingSystem.Models
{
    public class SalesInvoice
    {
        public int SalesInvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public int AccountId { get; set; }

        public Account? Account { get; set; } = null!;

        public ICollection<SalesInvoiceDetail> Details { get; set; }
            = new List<SalesInvoiceDetail>();
    }
}