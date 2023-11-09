using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalaryApi.DTO;

namespace SalaryApi.Controllers
{
    [Route("{datatype}/[controller]/[action]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        /// <summary>
        /// test contorller action
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="salaryData"></param>
        /// <returns></returns>
        [HttpGet]
        public  IActionResult test([FromRoute(Name = "datatype")] string datatype, [FromQuery] SalaryData salaryData )
        {
            
            return Ok(new { Datatype = datatype ,SalaryInfo=salaryData});
        }
    }
}
