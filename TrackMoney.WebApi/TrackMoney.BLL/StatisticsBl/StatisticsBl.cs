﻿using TrackMoney.Data.Models.Entities;
using TrackMoney.Data.Repos.Repos.Statistics;
using TrackMoney.Services.Jwt;

namespace TrackMoney.BLL.StatisticsBl
{
    public class StatisticsBl : IStatisticsBl
    {
        private readonly IStatisticsRepo _statisticsRepo;

        public StatisticsBl(IStatisticsRepo statisticsRepo)
        {
            _statisticsRepo = statisticsRepo;
        }

        public async Task<IEnumerable<KeyValuePair<string, double>>> GetTransactionAmountPerCategoryAndType(string jwt, TransactionType transactionType)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);

            return await _statisticsRepo.GetTransactionAmountPerCategoryAndType(userId, transactionType);
        }



        public async Task<object> GetUserTransactionsByPeriod(string jwt, int? year, int? month, int? day, string transactionType)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);

            return null;
        }
    }
}