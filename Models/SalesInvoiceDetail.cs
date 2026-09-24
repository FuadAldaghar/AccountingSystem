
namespace AccountingSystem.Models
{
    public class SalesInvoiceDetail
    {
        public int SalesInvoiceDetailId { get; set; }
        public int SalesInvoiceId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Total { get; private set; }

        public SalesInvoice SalesInvoice { get; set; } = null!;
        public Item Item { get; set; } = null!;
    }
}