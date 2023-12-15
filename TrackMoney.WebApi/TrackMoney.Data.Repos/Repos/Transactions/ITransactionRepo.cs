
using TrackMoney.BLL.Models.Messages.Requests.Transactions;

namespace TrackMoney.Data.Repos.Repos.Transactions
{
    public interface ITransactionRepo
    {
        Task<object> AddUsersTransaction(string userId, AddTransactionRequest request);
        Task<object> GetTransactionsByUserId(string userId, int pageNumber, int pageSize);
    }
}
