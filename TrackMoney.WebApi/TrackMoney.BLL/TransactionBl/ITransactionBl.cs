
namespace TrackMoney.BLL.TransactionBl
{
    public interface ITransactionBl
    {
        Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize);
    }
}
