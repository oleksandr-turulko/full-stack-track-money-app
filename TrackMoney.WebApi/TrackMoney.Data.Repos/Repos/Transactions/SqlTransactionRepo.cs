using Microsoft.EntityFrameworkCore;
using TrackMoney.BLL.Models.Messages.Requests.Transactions;
using TrackMoney.Data.Context;
using TrackMoney.Data.Models.Dtos.Transactions;
using TrackMoney.Data.Models.Entities;
using TrackMoney.Data.Repos.Abstract;
using TrackMoney.Services.CurrencyConverter;

namespace TrackMoney.Data.Repos.Repos.Transactions
{
    public class SqlTransactionRepo : BaseRepo, ITransactionRepo
    {
        public SqlTransactionRepo(TrackMoneyDbContext db) : base(db)
        {
        }

        public async Task<object> AddTransactionByUserId(string userId, int pageNumber, int pageSize)
         => _db.Transactions
                .Include(t => t.Currency)
                .Where(t => t.UserId.ToString() == userId)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionViewDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Value,
                    CurrencyCode = t.Currency.Code
                });


        public async Task<object> AddUsersTransaction(string userId, AddTransactionRequest request)
        {
            var newTransaction = new Transaction
            {

                Id = Guid.NewGuid(),
                Description = request.Description,
                Value = request.Value,
                Date = DateTime.Now,
                TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), request.TransactionType),
                UserId = Guid.Parse(userId)
            };

            var currency = await _db.Currencies.FirstOrDefaultAsync(c => string.Equals(c.Code, request.CurrencyCode));
            currency ??= await AddNewCurrency(request.CurrencyCode, request.Value);

            newTransaction.Currency = currency;
            _db.Add(newTransaction);

            return newTransaction;
        }

        private async Task<Currency> AddNewCurrency(string currencyCode, decimal value)
        {
            var newCurrency = new Currency
            {
                Id = Guid.NewGuid(),
                Code = currencyCode,
                ValueInUAH = CurrencyConverter.ConvertCurrency(value, currencyCode, "UAH").Result
            };

            return newCurrency;
        }

        public async Task<object> GetTransactionsByUserId(string userId, int pageNumber, int pageSize)
        {
            var usersTransactions = _db.Transactions
                .Include(t => t.Currency)
                .Where(t => t.UserId.ToString() == userId)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionViewDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Value,
                    CurrencyCode = t.Currency.Code
                });


            return usersTransactions;
        }
    }
}
