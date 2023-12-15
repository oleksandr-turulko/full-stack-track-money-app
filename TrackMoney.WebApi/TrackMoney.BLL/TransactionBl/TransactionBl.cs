﻿using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.Models.Messages.Requests.Transactions;
using TrackMoney.Data.Repos.Repos.Transactions;
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

        public async Task<object> GetUsersTransactions(string jwt, int pageNumber, int pageSize)
        {
            var userId = await JwtReader.GetIdFromJwt(jwt);
            var transactions = await _transactionRepo.GetTransactionsByUserId(userId, pageNumber, pageSize);



            return null;
        }
    }
}