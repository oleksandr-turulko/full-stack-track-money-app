using Microsoft.AspNetCore.Mvc;
using TrackMoney.BLL.Enums;
using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.Models.Messages.Enums;
using TrackMoney.BLL.Models.Messages.Requests.Users;
using TrackMoney.BLL.UserBl;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace TrackMoney.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBl _userBl;

        public UsersController(IUserBl userBl)
        {
            _userBl = userBl;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult> SignUp(SignInRequest request)
        {
            request.SignInType = SignInType.SignUp;

            var response = await _userBl.UserBlAction(request, UserBlActions.SignUp);

            if (response is BadResponse)
                return BadRequest(response);

            return Created("", response);
        }
        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            request.SignInType = SignInType.SignIn;
            if (request.UserName.Contains('@'))
            {
                request.Email = request.UserName;
                request.UserName = null;
            }

            var response = await _userBl.UserBlAction(request, UserBlActions.SignIn);

            if (response is BadResponse)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("google-auth")]
        public async Task<ActionResult> GoogleAuth([FromQuery] string token)
        {
            var payload = await ValidateAsync(token, new ValidationSettings
            {
                Audience = new[]
                {
                    "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"
                }
            });

            var result = await _userBl.UserBlAction(new SignInRequest
            {
                Email = payload.Email,
                ExternalId = payload.Subject,
                ExternalType = "GOOGLE",
                SignInType = SignInType.GoogleAuth
            }, UserBlActions.GoogleAuth);


            return Created("", result);
        }
    }
}
