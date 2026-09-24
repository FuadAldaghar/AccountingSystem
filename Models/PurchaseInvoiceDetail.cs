namespace AccountingSystem.Models
{
    public class PurchaseInvoiceDetail
    {
        public int PurchaseInvoiceDetailId { get; set; }

        public int PurchaseInvoiceId { get; set; }

        public int ItemId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Total { get; private set; }

        public PurchaseInvoice? PurchaseInvoice { get; set; }

        public Item? Item { get; set; }
    }
}