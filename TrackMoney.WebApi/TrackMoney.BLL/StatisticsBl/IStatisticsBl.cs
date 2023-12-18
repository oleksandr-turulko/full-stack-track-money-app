namespace TrackMoney.BLL.StatisticsBl
{
    public interface IStatisticsBl
    {
        Task<object> GetUserTransactionsByPeriod(string jwt, int? year, int? month, int? day, string transactionType);

        Task<IEnumerable<KeyValuePair<string, decimal>>> GetTransactionAmountPerCategoryAndType(string jwt, string transactionType, string currencyCode);
    }
}
