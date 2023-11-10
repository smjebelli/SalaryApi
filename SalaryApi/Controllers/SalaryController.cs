using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalaryApi.Common;
using SalaryApi.DTO.API;
using SalaryApi.DTO.Service;
using SalaryApi.Services;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalaryApi.Controllers
{
    [Route("{datatype}/[controller]/[action]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute(Name = "datatype")] string datatype, [FromForm] AddSalaryRequest salary)
        {
            SalaryDataType salaryDataType;
            if (!Enum.TryParse(datatype.ToUpper(), out salaryDataType))
            {
                return BadRequest("فرمت ورودی معتبر نیست");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                await salary.Data.CopyToAsync(ms);

                var bytes = ms.ToArray();

                string data = Encoding.UTF8.GetString(bytes);

                AddSalaryServiceData addSalaryServiceData = new AddSalaryServiceData()
                {
                    Data = data,
                    OverTimeCaluculator = salary.OverTimeCaluculator
                };

               await _salaryService.Add(addSalaryServiceData,salaryDataType);
                
            }


            return Ok(new { Datatype = datatype, SalaryInfo = salary });
        }
    }
}
