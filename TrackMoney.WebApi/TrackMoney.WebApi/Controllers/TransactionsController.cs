using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.Models.Messages.Requests.Transactions;
using TrackMoney.BLL.TransactionBl;

namespace TrackMoney.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionBl _transactionsBl;

        public TransactionsController(ITransactionBl transactionsBl)
        {
            _transactionsBl = transactionsBl;
        }

        [HttpPost("setBallance")]
        public async Task<ActionResult> SetBallance(decimal ballance)
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.SetUsersBallance(jwt, ballance);

            if (response is null)
            {
                return Ok();
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersTransactions(int pageSize = 10, int pageNumber = 1, string currency = "UAH")
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.GetUsersTransactions(jwt, pageNumber, pageSize, currency);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddUsersTransaction([FromBody] AddTransactionRequest request)
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.AddUsersTransaction(jwt, request);
            if (response is BadResponse)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateUsersTransaction([FromBody] UpdateTransactionRequest request)
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.UpdateUsersTransaction(jwt, request);
            if (response is BadResponse)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult> DeleteUsersTransactionById(string transactionId)
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.DeleteUsersTransactionById(jwt, transactionId);
            if (response is BadResponse)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
