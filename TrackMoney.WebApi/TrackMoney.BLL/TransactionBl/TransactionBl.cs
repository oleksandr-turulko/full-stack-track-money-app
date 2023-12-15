using TrackMoney.Data.Repos.Repos.Transactions;

namespace TrackMoney.BLL.TransactionBl
{
    public class TransactionBl : ITransactionBl
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionBl(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
