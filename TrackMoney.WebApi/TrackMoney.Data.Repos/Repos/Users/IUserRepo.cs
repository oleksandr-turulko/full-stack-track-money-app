
using TrackMoney.BLL.Models.Messages.Requests.Users;
using TrackMoney.Data.Models.Entities;
namespace TrackMoney.Data.Repos.Repos.Users
{
    public interface IUserRepo
    {
        Task<User> GetUserByExternalId(string? externalId, string? externalType);
        Task<User> GetUserBySignInRequest(SignInRequest request);
        Task<User> SignUpUser(SignInRequest request);
    }
}
