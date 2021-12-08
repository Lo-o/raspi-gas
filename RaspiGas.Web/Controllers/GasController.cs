using Microsoft.AspNetCore.Mvc;
using RaspiGas.SensorDataAccess;

namespace RaspiGas.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GasController : ControllerBase
    {
        private readonly TimescaleHelper _timescaleHelper;

        public GasController()
        {
            _timescaleHelper = new TimescaleHelper();
        }

        [HttpGet("GetGas")]
        public async Task<IActionResult> GetGas()
        {
            _timescaleHelper.CheckDatabaseConnection();

            return Ok();
        }
    }
}
