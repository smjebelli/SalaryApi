using Microsoft.EntityFrameworkCore;
using OvetimePolicies;
using SalaryApi.Common;
using SalaryApi.DTO.API;
using SalaryApi.DTO.Service;
using SalaryApi.Models;
using System.Runtime.CompilerServices;

namespace SalaryApi.Services
{
    public interface ISalaryService
    {
        Task<GeneralBaseResponse> Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType);
        Task<GeneralBaseResponse> Update(SalaryData salaryData);
        Task<GeneralBaseResponse> Delete(SalaryData salaryData);
        Task<GeneralBaseResponse<SalaryData>> Get(long employeeId);
        Task<GeneralBaseResponse<IEnumerable<SalaryData>>> GetRange(long employeeId);
    }
    public class SalaryService : ISalaryService
    {
        private AppDbContext _context;
        private readonly ILogger<SalaryService> _logger;

        public const long tax = 1000;


        public SalaryService(AppDbContext context, ILogger<SalaryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GeneralBaseResponse> Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType)
        {
            var response = new GeneralBaseResponse();

            SalaryParserHandler salaryDataParser = null;

            switch (salaryDataType)
            {
                case SalaryDataType.JSON:

                    break;
                case SalaryDataType.XML:
                    break;
                case SalaryDataType.CSV:
                    break;
                case SalaryDataType.CUSTOM:
                    salaryDataParser = new SalaryParserHandler(new CustomSalaryParser(salaryRawInput.Data));
                    break;
                default:
                    break;
            }
            if (salaryDataParser is null)
            {
                response.Result = NodeResult.InputSalaryFormatNotValid;
                return response;
            }

            var salaryData = salaryDataParser.Parse();

            var emp_date = salaryData.Select(x => $"{x.EmployeeId}_{x.Date}").ToList();

            // check if any of records exists in salarydata            
            string records = "'" + string.Join("','", emp_date) + "'";

            string query = $"select * from Salary where Cast(EmployeeId as varchar)+'_'+[Date] in ({records})";

            var dups = _context.Salary.FromSqlRaw(query).ToList();

            foreach (var item in salaryData)
            {                
                if (dups.Any(x => (x.EmployeeId + "_" + x.Date) == (item.EmployeeId + "_" + item.Date) ))
                {
                   _logger.LogWarning($"salary insert ignored - {item.EmployeeId}_{item.Date} already exists in database");
                    continue;
                }
                long overtimeAmount = new CalculatorCreator(salaryRawInput.OverTimeCaluculator, item.BasicSalary, item.Allowance)
                        .CreateCalculator()
                        .Calculate();

                _context.Salary.Add(new Salary()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EmployeeId = item.EmployeeId,
                    BasicSalary = item.BasicSalary,
                    Allowance = item.Allowance,
                    Transportation = item.Transportation,
                    Date = item.Date,
                    ComputedSalary = item.ComputeSalary(overtimeAmount, tax)

                }); ;
            }
            int res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                response.Result = NodeResult.Ok;
                return response;
            }
            else
            {
                response.Result = BaseResult.NotContent;
                return response;
            }

        }

        public Task<GeneralBaseResponse> Update(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralBaseResponse> Delete(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralBaseResponse<SalaryData>> Get(long employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralBaseResponse<IEnumerable<SalaryData>>> GetRange(long employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
