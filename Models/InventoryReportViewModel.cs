namespace AccountingSystem.Models
{
    public class InventoryReportViewModel
    {
        public int ItemId { get; set; }

        public string ItemNumber { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        public decimal PurchasedQuantity { get; set; }

        public decimal SoldQuantity { get; set; }

        public decimal StockQuantity => PurchasedQuantity - SoldQuantity;
    }
}