using TrackMoney.Data.Context;

namespace TrackMoney.Data.Repos.Abstract
{
    public abstract class BaseRepo
    {
        protected readonly TrackMoneyDbContext _db;

        public BaseRepo(TrackMoneyDbContext db)
        {
            _db = db;
        }
    }
}
