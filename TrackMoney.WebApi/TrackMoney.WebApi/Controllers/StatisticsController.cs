using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMoney.BLL.StatisticsBl;

namespace TrackMoney.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        public StatisticsController(IStatisticsBl statisticsBl)
        {
            _statisticsBl = statisticsBl;
        }
    }
}
