using TrackMoney.Data.Models.Entities;

namespace TrackMoney.Data.Repos.Repos.Statistics
{
    public interface IStatisticsRepo
    {
        Task<IEnumerable<KeyValuePair<string, double>>> GetTransactionAmountPerCategoryAndType(string userId, TransactionType transactionType);
        Task<decimal> GetBallance(string userId);

        public Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod(Guid userId, int? year, int? month,
            int? day, string transactionType);
    }
}
