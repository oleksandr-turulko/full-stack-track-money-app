namespace TrackMoney.BLL.Models.Messages.Requests.Transactions
{
    public class AddTransactionRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string CurrencyCode { get; set; }

        //Income or Expense
        public string TransactionType { get; set; }

        public async Task<bool> IsValid()
        => !string.IsNullOrEmpty(Description) && Value > 0
            && !string.IsNullOrEmpty(CurrencyCode) && !string.IsNullOrEmpty(TransactionType);
    }
}
