using TrackMoney.BLL.Models.Messages.Enums;

namespace TrackMoney.BLL.Models.Messages.Requests.Users
{
    public class SignInRequest
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? ExternalId { get; set; }
        public string? ExternalType { get; set; }

        public SignInType SignInType { get; set; }

        public async Task<bool> IsValidModelFor()
        {
            bool flag = false;
            switch (SignInType)
            {
                case SignInType.SignIn:
                    flag = await IsValidForSignIn();
                    break;
                case SignInType.SignUp:
                    flag = await IsValidForSignUp();
                    break;
                case SignInType.GoogleAuth:
                    flag = await IsValidForGoogleAuth();
                    break;
            }
            return flag;
        }

        private async Task<bool> IsValidForSignIn()
        => (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(UserName)) && !string.IsNullOrWhiteSpace(Password);


        private async Task<bool> IsValidForSignUp()
        => (!string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(UserName)) && !string.IsNullOrWhiteSpace(Password);


        private async Task<bool> IsValidForGoogleAuth()
        => !string.IsNullOrWhiteSpace(ExternalId) && !string.IsNullOrWhiteSpace(ExternalId);



    }
}
