namespace AccountingSystem.Models
{
    public class AccountStatementViewModel
    {
        public DateTime Date { get; set; }

        public string OperationType { get; set; } = string.Empty;

        public string ReferenceNumber { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }
}