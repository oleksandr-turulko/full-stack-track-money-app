
using TrackMoney.BLL.Models.Messages.Requests.Transactions;

namespace TrackMoney.BLL.TransactionBl
{
    public interface ITransactionBl
    {
        Task<object> AddUsersTransaction(string jwt, AddTransactionRequest request);
        Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize);
    }
}
