using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMoney.BLL.Models.Messages;
using TrackMoney.BLL.StatisticsBl;

namespace TrackMoney.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsBl _statisticsBl;

        public StatisticsController(IStatisticsBl statisticsBl)
        {
            _statisticsBl = statisticsBl;
        }


        [HttpGet]
        public async Task<ActionResult> GetUserTransactionsByPeriod(int? year, int? month,
                                                                    int? day, string transactionType = "Expense")
        {
            var jwt = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var dataByPeriod = await _statisticsBl.GetUserTransactionsByPeriod(jwt, year, month, day, transactionType);

            if (dataByPeriod is BadResponse)
            {
                return BadRequest(dataByPeriod);
            }

            return Ok(dataByPeriod);
        }
    }
}