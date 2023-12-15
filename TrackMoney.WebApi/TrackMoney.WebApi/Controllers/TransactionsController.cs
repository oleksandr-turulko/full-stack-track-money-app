using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return Ok();
        }
    }
}
