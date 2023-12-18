using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.Models.Messages.Requests.Transactions;
using TrackMoney.Data.Models.Dtos.Transactions;
using TrackMoney.Data.Repos.Repos.Transactions;
using TrackMoney.Services.CurrencyConverter;
using TrackMoney.Services.Jwt;

namespace TrackMoney.BLL.TransactionBl
{
    public class TransactionBl : ITransactionBl
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionBl(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }




        public async Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize, string currency)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);
            var transactions = await _transactionRepo.GetTransactionsByUserId(userId, pageNumber, pageSize);
            var convertedTransactions = new List<TransactionViewDto>(transactions);
            foreach (var transaction in convertedTransactions)
            {
                transaction.Amount =
                    CurrencyConverter.ConvertCurrency(transaction.Amount, "UAH", currency).Result;
                transaction.CurrencyCode = currency;
            }

            return transactions;
        }

        public async Task<object> UpdateUsersTransaction(string jwt, UpdateTransactionRequest request)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);
            var transactionById = await _transactionRepo.GetUsersTransactionById(userId, request.TransactionId);
            if (transactionById != null)
            {
                if (await request.IsValid())
                {
                    var result = await _transactionRepo.UpdateUsersTransaction(transactionById, request);
                    return result;
                }


                return new BadResponse
                {
                    Message = "Invalid data"
                };
            }

            return new BadResponse
            {
                Message = "You dont have transaction with that id"
            };
        }

        public async Task<object> AddUsersTransaction(string jwt, AddTransactionRequest request)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);
            if (await request.IsValid())
            {
                var result = await _transactionRepo.AddUsersTransaction(userId, request);
                return result;
            }


            return new BadResponse
            {
                Message = "Invalid data"
            };
        }

        public async Task<object?> DeleteUsersTransactionById(string jwt, string transactionId)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);
            var transactionById = await _transactionRepo.GetUsersTransactionById(userId, transactionId);

            if (transactionById != null)
            {
                await _transactionRepo.RemoveUsersTransaction(transactionById);
                return null;
            }

            return new BadResponse
            {
                Message = "Invalid data"
            };
        }

        public async Task<object?> SetUsersBallance(string jwt, decimal ballance)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);

            if (ballance > 0)
            {
                await _transactionRepo.SetUsersBallance(userId, ballance);
                return null;
            }

            return new BadResponse
            {
                Message = "Ballance cannot be lower than 0"
            };
        }
    }
}
