using TrackMoney.BLL.Enums;
using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.Models.Messages.Requests.Users;
using TrackMoney.BLL.Models.Messages.Responses.Users;
using TrackMoney.Data.Models.Entities;
using TrackMoney.Data.Repos.Repos.Users;
using TrackMoney.Services.Jwt;

namespace TrackMoney.BLL.UserBl
{
    public class UserBl : IUserBl
    {
        private readonly IUserRepo _userRepo;

        public UserBl(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }



        public async Task<object> UserBlAction(SignInRequest request, UserBlActions action)
        {
            User user;

            switch (action)
            {
                case UserBlActions.SignIn:
                    user = (User)await SignIn(request);
                    break;
                case UserBlActions.SignUp:
                    user = (User)await SignUp(request);
                    break;
                case UserBlActions.GoogleAuth:
                    user = (User)await GoogleAuth(request);
                    break;
                default:
                    user = (User)await SignIn(request);
                    break;
            }

            return new AuthResponse
            {
                Jwt = JwtGenerator.GenerateJWT(user.Username, user.Id)
            };
        }

        private async Task<object> SignIn(SignInRequest request)
        {
            var userBySignInCredentials = await _userRepo.GetUserBySignInRequest(request);
            if (userBySignInCredentials == null)
            {
                return new BadResponse
                {
                    Message = "Incorrect username or pasword"
                };
            }

            return new AuthResponse
            {
                Jwt = JwtGenerator.GenerateJWT(userBySignInCredentials.Username, userBySignInCredentials.Id)
            };
        }

        private async Task<object> SignUp(SignInRequest request)
        {
            var userBySignUpCredentials = await _userRepo.GetUserBySignInRequest(request);
            if (userBySignUpCredentials != null)
            {
                return new BadResponse
                {
                    Message = "User with same credentials already exists"
                };
            }
            var newUser = await _userRepo.SignUpUser(request);

            return new AuthResponse
            {
                Jwt = JwtGenerator.GenerateJWT(newUser.Username, newUser.Id)
            };
        }

        private async Task<object> GoogleAuth(SignInRequest request)
        {
            var userByGoogleCredentials = await _userRepo.GetUserByExternalId(request.ExternalId, request.ExternalType);

            if (userByGoogleCredentials == null)
            {
                return await SignUp(request);
            }
            return await SignIn(request);
        }

    }
}
