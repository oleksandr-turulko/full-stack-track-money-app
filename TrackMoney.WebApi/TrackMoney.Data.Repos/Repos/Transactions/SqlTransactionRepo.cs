using TrackMoney.Data.Context;
using TrackMoney.Data.Repos.Abstract;

namespace TrackMoney.Data.Repos.Repos.Transactions
{
    public class SqlTransactionRepo : BaseRepo, ITransactionRepo
    {
        public SqlTransactionRepo(TrackMoneyDbContext db) : base(db)
        {
        }
    }
}
