using TrackMoney.BLL.Enums;
using TrackMoney.BLL.Models.Messages.Requests.Users;

namespace TrackMoney.BLL.UserBl
{
    public interface IUserBl
    {

        public Task<object> UserBlAction(SignInRequest request, UserBlActions action);
    }
}
