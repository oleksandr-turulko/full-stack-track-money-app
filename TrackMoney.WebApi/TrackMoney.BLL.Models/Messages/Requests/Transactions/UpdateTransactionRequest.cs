namespace TrackMoney.BLL.Models.Messages.Requests.Transactions
{
    public class UpdateTransactionRequest : AddTransactionRequest
    {
        public string TransactionId { get; set; }
    }
}
