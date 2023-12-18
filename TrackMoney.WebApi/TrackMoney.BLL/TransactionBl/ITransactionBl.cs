using TrackMoney.BLL.Models.Messages.Requests.Transactions;

namespace TrackMoney.BLL.TransactionBl
{
    public interface ITransactionBl
    {
        Task<object> UpdateUsersTransaction(string jwt, UpdateTransactionRequest request);
        Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize, string currency);
        Task<object> AddUsersTransaction(string jwt, AddTransactionRequest request);
        Task<object?> DeleteUsersTransactionById(string jwt, string transactionId);
        Task<object?> SetUsersBallance(string jwt, decimal ballance);
    }
}
