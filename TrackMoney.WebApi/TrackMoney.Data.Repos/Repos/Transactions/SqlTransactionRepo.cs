using AutoMapper;
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
        private readonly IMapper _mapper;

        public SqlTransactionRepo(TrackMoneyDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }


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
            await _db.SaveChangesAsync();

            return _mapper.Map<TransactionViewDto>(newTransaction);
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
            var take = pageNumber * pageSize;
            var skip = pageNumber == 1 ? 0 : pageNumber * pageSize;
            var usersTransactions = _db.Transactions
                .Include(t => t.Currency)
                .Where(t => t.UserId.ToString() == userId)
                .Skip(skip)
                .Take(take)
                .AsEnumerable();
            return _mapper.Map<IEnumerable<TransactionViewDto>>(usersTransactions);
        }

        public async Task<object> UpdateUsersTransaction(Transaction transaction, UpdateTransactionRequest request)
        {
            transaction.TransactionType = Enum.Parse<TransactionType>(request.TransactionType);
            transaction.Description = request.Description;
            transaction.Value = request.Value;
            transaction.UpdatedAt = DateTime.Now;
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();


            return _mapper.Map<TransactionViewDto>(transaction);
        }

        public async Task<Transaction?> GetUsersTransactionById(string userId, string transactionId)
        => await _db.Transactions
            .FirstOrDefaultAsync(t => string.Equals(userId, t.UserId.ToString()) &&
                                                        string.Equals(transactionId, t.Id.ToString()));

        public async Task RemoveUsersTransaction(Transaction transactionById)
        {
            _db.Remove(transactionById);
            await _db.SaveChangesAsync();
        }
    }
}
