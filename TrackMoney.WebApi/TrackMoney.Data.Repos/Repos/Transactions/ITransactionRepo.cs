using TrackMoney.BLL.Models.Messages.Requests.Transactions;
using TrackMoney.Data.Models.Entities;

namespace TrackMoney.Data.Repos.Repos.Transactions
{
    public interface ITransactionRepo
    {
        Task<Transaction?> GetUsersTransactionById(string userId, string transactionId);
        Task<object> AddUsersTransaction(string userId, AddTransactionRequest request);
        Task<object> GetTransactionsByUserId(string userId, int pageNumber, int pageSize);
        Task<object> UpdateUsersTransaction(Transaction transaction, UpdateTransactionRequest request);
    }
}
