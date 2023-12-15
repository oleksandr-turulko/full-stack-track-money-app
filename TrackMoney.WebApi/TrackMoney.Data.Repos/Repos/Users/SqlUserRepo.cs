using Microsoft.AspNet.Identity;
using TrackMoney.BLL.Models.Messages.Requests.Users;
using TrackMoney.Data.Context;
using TrackMoney.Data.Models.Entities;
using TrackMoney.Data.Repos.Abstract;

namespace TrackMoney.Data.Repos.Repos.Users
{
    public class SqlUserRepo : BaseRepo, IUserRepo
    {
        private readonly IPasswordHasher _passwordHasher;

        public SqlUserRepo(TrackMoneyDbContext db, IPasswordHasher passwordHasher) : base(db)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetUserByExternalId(string externalId, string externalType)
        => _db.Users.FirstOrDefault(u =>
            string.Equals(u.ExternalAppId, externalId)
        && string.Equals(u.ExternalAppType, externalType));

        public async Task<User> GetUserBySignInRequest(SignInRequest request)
        {
            var user = _db.Users
                   .FirstOrDefault(u =>
                   string.Equals(u.Username, request.UserName)
                   || string.Equals(u.Email, request.Email));

            if (user != null && _passwordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Success)
            {
                return user;
            }


            return null;
        }

        public async Task<User> SignUpUser(SignInRequest request)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Username = request.UserName,
                Password = _passwordHasher.HashPassword(request.Password)
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            return newUser;
        }
    }
}
