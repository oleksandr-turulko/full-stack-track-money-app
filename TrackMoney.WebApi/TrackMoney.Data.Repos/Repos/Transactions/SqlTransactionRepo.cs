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
            var user = await _db.Users.Include(u => u.Transactions).FirstOrDefaultAsync(u => string.Equals(u.Id.ToString(), userId));


            var newTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                Value = CurrencyConverter.ConvertCurrency(request.Value, request.CurrencyCode, "UAH").Result,
                Date = DateTime.Now,
                TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), request.TransactionType),
                UserId = Guid.Parse(userId)
            };

            user.Ballance += newTransaction.TransactionType == TransactionType.Income
                                                    ? newTransaction.Value
                                                    : -newTransaction.Value;
            user.Transactions.Add(newTransaction);


            _db.Update(user);
            await _db.SaveChangesAsync();

            return _mapper.Map<TransactionViewDto>(newTransaction);
        }


        public async Task<IEnumerable<TransactionViewDto>> GetTransactionsByUserId(string userId, int pageNumber, int pageSize)
        {
            var take = pageNumber * pageSize;
            var skip = pageNumber == 1 ? 0 : pageNumber * pageSize;
            var usersTransactions = _db.Transactions
                .Where(t => t.UserId.ToString() == userId)
                .Skip(skip)
                .Take(take)
                .ToList();

            return _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewDto>>(usersTransactions);
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

        public async Task SetUsersBallance(string userId, decimal ballance)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => string.Equals(userId, u.Id.ToString()));
            user.Ballance = ballance;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
