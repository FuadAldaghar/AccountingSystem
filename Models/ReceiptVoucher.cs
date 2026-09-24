namespace AccountingSystem.Models
{
    public class ReceiptVoucher
    {
        public int ReceiptVoucherId { get; set; }
        public string VoucherNumber { get; set; } = string.Empty;
        public DateTime VoucherDate { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public string? Notes { get; set; }

        public Account? Account { get; set; } = null!;
    }
}