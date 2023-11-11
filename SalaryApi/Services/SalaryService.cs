using Microsoft.EntityFrameworkCore;
using OvetimePolicies;
using SalaryApi.Common;
using SalaryApi.Configurations;
using SalaryApi.DTO.API;
using SalaryApi.DTO.API.Response;
using SalaryApi.DTO.Service;
using SalaryApi.Models;
using System.Runtime.CompilerServices;

namespace SalaryApi.Services
{
    public interface ISalaryService
    {
        Task<GeneralBaseResponse> Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType);
        Task<GeneralBaseResponse> Update(UpdateSalaryServiceData updateSalaryData);
        Task<GeneralBaseResponse> Delete(GetSalaryServiceData delSalaryData);
        Task<GeneralBaseResponse<GetSalaryResponse>> Get(GetSalaryServiceData getSalaryData);
        Task<GeneralBaseResponse<IEnumerable<GetSalaryResponse>>> GetRange(GetSalaryRangeServiceData getSalaryData);
    }
    public class SalaryService : ISalaryService
    {
        private AppDbContext _context;
        private readonly ILogger<SalaryService> _logger;
        private IConfiguration _configuration;

        private long tax = 0;

        public SalaryService(AppDbContext context, ILogger<SalaryService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            var salaryConf = configuration.GetSection("Salary").Get<SalaryConfiguration>();
            if (salaryConf != null)
                tax = salaryConf.Tax;
            else
                tax = 800;

        }

        public async Task<GeneralBaseResponse> Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType)
        {
            var response = new GeneralBaseResponse();

            var salaryDataParser = HandleSalaryParser(salaryDataType, salaryRawInput.Data);

            if (salaryDataParser is null)
            {
                response.Result = NodeResult.InputSalaryFormatNotValid;
                return response;
            }

            var salaryData = salaryDataParser.Parse();

            var emp_date = salaryData.Select(x => $"{x.EmployeeId}_{x.Date}").ToList();

            // check if any of records exists in salarydata            
            string records = "'" + string.Join("','", emp_date) + "'";

            string query = $"select * from Salary where Cast([{nameof(SalaryData.EmployeeId)}] as varchar)+'_'+[{nameof(SalaryData.Date)}] in ({records})";

            var dups = _context.Salary.FromSqlRaw(query).ToList();

            foreach (var item in salaryData)
            {
                if (dups.Any(x => (x.EmployeeId + "_" + x.Date) == (item.EmployeeId + "_" + item.Date)))
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
                response.Result = NodeResult.NoRecordsAffected;
                return response;
            }

        }



        public async Task<GeneralBaseResponse> Update(UpdateSalaryServiceData updateSalaryData)
        {
            var response = new GeneralBaseResponse();

            var salaryDataDb = await _context.Salary.FirstOrDefaultAsync(
                x => x.EmployeeId == updateSalaryData.EmployeeId && x.Date == updateSalaryData.Date);


            if (salaryDataDb == null)
            {
                response.Result = NodeResult.SalaryDataNotFound;
                return response;
            }

            long overtimeAmount = new CalculatorCreator(updateSalaryData.OverTimeCaluculator, updateSalaryData.BasicSalary, updateSalaryData.Allowance)
                        .CreateCalculator()
                        .Calculate();



            salaryDataDb.EmployeeId = updateSalaryData.EmployeeId;
            salaryDataDb.Allowance = updateSalaryData.Allowance;
            salaryDataDb.BasicSalary = updateSalaryData.BasicSalary;
            salaryDataDb.Date = updateSalaryData.Date;
            salaryDataDb.FirstName = updateSalaryData.FirstName;
            salaryDataDb.LastName = updateSalaryData.LastName;
            salaryDataDb.Transportation = updateSalaryData.Transportation;
            salaryDataDb.ComputedSalary = updateSalaryData.ComputeSalary(overtimeAmount, tax);


            _context.Salary.Update(salaryDataDb);

            int res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                response.Result = NodeResult.Ok;
                return response;
            }
            else
            {
                response.Result = NodeResult.NoRecordsAffected;
                return response;
            }

        }

        public async Task<GeneralBaseResponse> Delete(GetSalaryServiceData delSalaryData)
        {
            var response = new GeneralBaseResponse();

            var salaryRecord = await _context.Salary.FirstOrDefaultAsync(x => x.EmployeeId == delSalaryData.EmployeeId && x.Date == delSalaryData.Date);

            if (salaryRecord is null)
            {
                response.Result = NodeResult.SalaryDataNotFound;
                return response;
            }

            _context.Salary.Remove(salaryRecord);

            var res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                response.Result = NodeResult.Ok;
                return response;
            }
            else
            {
                response.Result = NodeResult.NoRecordsAffected;
                return response;
            }
        }

        public async Task<GeneralBaseResponse<GetSalaryResponse>> Get(GetSalaryServiceData getSalaryData)
        {
            var response = new GeneralBaseResponse<GetSalaryResponse>();

            var salaryRecord = await _context.Salary.FirstOrDefaultAsync(x => x.EmployeeId == getSalaryData.EmployeeId && x.Date == getSalaryData.Date);

            if (salaryRecord is null)
            {
                response.Result = NodeResult.SalaryDataNotFound;
                return response;
            }

            response.Data = new GetSalaryResponse()
            {
                Allowance = salaryRecord.Allowance,
                BasicSalary = salaryRecord.BasicSalary,
                Date = salaryRecord.Date,
                EmployeeId = salaryRecord.EmployeeId,
                FirstName = salaryRecord.FirstName,
                LastName = salaryRecord.LastName,
                Transportation = salaryRecord.Transportation,
                ComputedSalary = salaryRecord.ComputedSalary,
            };

            response.Result = NodeResult.Ok;
            return response;

        }

        public async Task<GeneralBaseResponse<IEnumerable<GetSalaryResponse>>> GetRange(GetSalaryRangeServiceData getSalaryData)
        {
            var response = new GeneralBaseResponse<IEnumerable<GetSalaryResponse>>();

            var salaryRecord = await _context.Salary.Where(
                x => x.EmployeeId == getSalaryData.EmployeeId &&
                 (x.Date.CompareTo(getSalaryData.DateStart) >= 0 && x.Date.CompareTo(getSalaryData.DateEnd) <= 0)).ToListAsync();

            if (salaryRecord is null)
            {
                response.Result = NodeResult.SalaryDataNotFound;
                return response;
            }

            response.Data = salaryRecord.Select(x => new GetSalaryResponse()
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                Allowance = x.Allowance,
                BasicSalary = x.BasicSalary,
                ComputedSalary = x.ComputedSalary,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Transportation = x.Transportation
            });

            response.Result = NodeResult.Ok;
            return response;
        }

        private SalaryParserHandler HandleSalaryParser(SalaryDataType salaryDataType, string data)
        {
            SalaryParserHandler salaryDataParser = null;
            switch (salaryDataType)
            {
                case SalaryDataType.JSON:
                    salaryDataParser = new SalaryParserHandler(new JsonSalaryParser(data));

                    break;
                case SalaryDataType.XML:
                    salaryDataParser = new SalaryParserHandler(new XmlSalaryParser(data));

                    break;
                case SalaryDataType.CSV:
                    salaryDataParser = new SalaryParserHandler(new CsvSalaryParser(data));
                    break;
                case SalaryDataType.CUSTOM:
                    salaryDataParser = new SalaryParserHandler(new CustomSalaryParser(data));
                    break;
                default:
                    break;
            }

            return salaryDataParser;
        }
    }
}
