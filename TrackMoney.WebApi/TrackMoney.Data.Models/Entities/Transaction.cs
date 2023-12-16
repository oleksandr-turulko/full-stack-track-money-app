namespace TrackMoney.Data.Models.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Currency Currency { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
