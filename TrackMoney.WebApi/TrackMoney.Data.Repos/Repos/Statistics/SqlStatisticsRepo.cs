using TrackMoney.Data.Context;
using TrackMoney.Data.Repos.Abstract;

namespace TrackMoney.Data.Repos.Repos.Statistics
{
    public class SqlStatisticsRepo : BaseRepo, IStatisticsRepo
    {
        public SqlStatisticsRepo(TrackMoneyDbContext db) : base(db)
        {
        }


    }
}
