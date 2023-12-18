using TrackMoney.Data.Models.Entities;

namespace TrackMoney.Data.Repos.Repos.Statistics
{
    public interface IStatisticsRepo
    {
        Task<IEnumerable<KeyValuePair<string, decimal>>> GetTransactionAmountPerCategoryAndType(string userId, string transactionType, string currencyCode);
        Task<decimal> GetBallance(string userId);

        public Task<IEnumerable<Transaction>> GetUserTransactionsByPeriod(Guid userId, int? year, int? month,
            int? day, string transactionType);
    }
}
