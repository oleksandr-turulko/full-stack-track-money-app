namespace TrackMoney.Data.Models.Dtos.Transactions
{
    public class TransactionViewDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
