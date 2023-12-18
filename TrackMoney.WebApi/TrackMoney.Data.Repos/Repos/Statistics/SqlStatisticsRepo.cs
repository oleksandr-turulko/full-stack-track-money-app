using TrackMoney.Data.Context;
using TrackMoney.Data.Models.Entities;
using TrackMoney.Data.Repos.Abstract;

namespace TrackMoney.Data.Repos.Repos.Statistics
{
    public class SqlStatisticsRepo : BaseRepo, IStatisticsRepo
    {
        public SqlStatisticsRepo(TrackMoneyDbContext db) : base(db)
        {
        }

        public async Task<decimal> GetBallance(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => string.Equals(u.Id.ToString(), userId));
            var ballance = user == null ? user.Ballance : 0;
            return ballance;
        }


        public async Task<IEnumerable<KeyValuePair<string, decimal>>> GetTransactionAmountPerCategoryAndType(string userId, string transactionType, string currencyCode)
        {
            var transactionTypeFromString = Enum.Parse<TransactionType>(transactionType);
            var result = _db.Transactions
                    .Where(t => t.UserId == Guid.Parse(userId) && t.TransactionType == transactionTypeFromString)
                    .GroupBy(t => t.Description)
                    .Select(group => new KeyValuePair<string, decimal>(
                        group.Key,
                        group.Sum(t => (decimal)t.Value)
                    ))
                    .ToList();
            return (IEnumerable<KeyValuePair<string, decimal>>)result;
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod(Guid userId, int? year, int? month, int? day, string transactionType)
        {
            var query = _db.Transactions
                .Where(t => t.UserId == userId);

            if (year.HasValue)
                query = query.Where(t => t.Date.Year == year);

            if (month.HasValue)
                query = query.Where(t => t.Date.Month == month);

            if (day.HasValue)
                query = query.Where(t => t.Date.Day == day);

            if (!string.IsNullOrEmpty(transactionType))
            {
                var type = Enum.Parse<TransactionType>(transactionType, true);
                query = query.Where(t => t.TransactionType == type);
            }

            return query.AsEnumerable();
        }
    }
}
