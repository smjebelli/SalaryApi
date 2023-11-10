using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using SalaryApi.Common;
using SalaryApi.DTO.API;
using SalaryApi.DTO.Service;
using SalaryApi.Models;
using SalaryApi.Services;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalaryApi.Controllers
{
    [Route("{datatype}/[controller]/[action]")]
    [ApiController]
    public class SalaryController : ApiControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService, IConfiguration configuration,
            IHttpContextAccessor accessor) : base(configuration, accessor)
        {
            _salaryService = salaryService;
        }
        private async Task <string> FileDateToString(IFormFile formFile)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await formFile.CopyToAsync(ms);
                var bytes = ms.ToArray();
                return Encoding.UTF8.GetString(bytes);
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute(Name = "datatype")] string datatype, [FromForm] AddSalaryRequest salary)
        {
            SalaryDataType salaryDataType;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Enum.TryParse(datatype.ToUpper(), out salaryDataType))
            {
                return BadRequest("Input format is not valid");
            }
            
            AddSalaryServiceData addSalaryServiceData = new AddSalaryServiceData()
            {
                Data =await FileDateToString(salary.Data),
                OverTimeCaluculator = salary.OverTimeCaluculator
            };

            var res = await _salaryService.Add(addSalaryServiceData, salaryDataType);

            return GeneralResponse(res);
        }
    }
}
