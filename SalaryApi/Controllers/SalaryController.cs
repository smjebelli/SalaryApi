using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using SalaryApi.Common;
using SalaryApi.DTO.API.Request;
using SalaryApi.DTO.Service;
using SalaryApi.Models;
using SalaryApi.Services;
using System.Text;

namespace SalaryApi.Controllers
{
    
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
        /// <summary>
        /// افزودن رکورد جدید حقوق
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{datatype}/[controller]/[action]")]
        public async Task<IActionResult> Add([FromRoute(Name = "datatype")] string datatype, [FromForm] AddSalaryRequest salary)
        {
            SalaryDataType salaryDataType;

            if (!ModelState.IsValid)            
                return BadRequest(ModelState);
            
            if (!Enum.TryParse(datatype.ToUpper(), out salaryDataType))            
                return BadRequest("Input format is not valid");
            
            
            AddSalaryServiceData addSalaryServiceData = new AddSalaryServiceData()
            {
                Data =await FileDateToString(salary.Data),
                OverTimeCaluculator = salary.OverTimeCaluculator
            };

            var res = await _salaryService.Add(addSalaryServiceData, salaryDataType);

            return GeneralResponse(res);
        }
        /// <summary>
        /// بروز رسانی رکورد حقوق یک کارمند در تاریخ مشخص
        /// </summary>
        /// <param name="salaryRequest"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateSalaryRequest salaryRequest)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);
                    

            UpdateSalaryServiceData data = new UpdateSalaryServiceData()
            {
                Allowance = salaryRequest.Allowance,
                BasicSalary = salaryRequest.BasicSalary,
                Date = salaryRequest.Date,
                EmployeeId = salaryRequest.EmployeeId,
                FirstName = salaryRequest.FirstName,
                LastName = salaryRequest.LastName,
                Transportation = salaryRequest.Transportation,
                OverTimeCaluculator = salaryRequest.OverTimeCaluculator
            };
            var res = await _salaryService.Update(data);
            
            return GeneralResponse(res);

        }

        /// <summary>
        /// دریافت اطلاعات حقوق یک کارمند در تاریخ مشخص
        /// </summary>
        /// <param name="salaryRequest"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSalaryRequest salaryRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = new GetSalaryServiceData()
            {
                Date = salaryRequest.Date,
                EmployeeId = salaryRequest.EmployeeId
            };

            var res = await _salaryService.Get(data);
            
            return GeneralResponse(res);

        }

        /// <summary>
        /// دریافت رکوردهای حقوق یک کارمند در بازه زمانی مشخص
        /// </summary>
        /// <param name="salaryRequest"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> GetRange([FromQuery] GetSalaryRangeRequest salaryRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = new GetSalaryRangeServiceData()
            {
                DateStart = salaryRequest.DateStart,
                DateEnd = salaryRequest.DateEnd,
                EmployeeId = salaryRequest.EmployeeId
            };

            var res = await _salaryService.GetRange(data);

            return GeneralResponse(res);

        }

        /// <summary>
        /// حذف رکورد حقوق یک کارمند در بازه زمانی مشخص
        /// </summary>
        /// <param name="salaryRequest"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] GetSalaryRequest salaryRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = new GetSalaryServiceData()
            {
                Date = salaryRequest.Date,
                EmployeeId = salaryRequest.EmployeeId
            };

            var res = await _salaryService.Delete(data);

            return GeneralResponse(res);

        }
    }
}
