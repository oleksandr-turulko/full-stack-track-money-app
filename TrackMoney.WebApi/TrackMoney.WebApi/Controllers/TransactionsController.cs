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

        [HttpGet]
        public async Task<ActionResult> GetUsersTransactions(int pageSize = 10, int pageNumber = 1)
        {

            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var response = await _transactionsBl.GetUsersTransactions(jwt, pageNumber, pageSize);
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
    }
}
