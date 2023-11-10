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
        Task Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType);
        Task Update(SalaryData salaryData);
        Task Delete(SalaryData salaryData);
        Task<SalaryData> Get(long employeeId);
        Task<IEnumerable<SalaryData>> GetRange(long employeeId);
    }
    public class SalaryService : ISalaryService
    {
        private AppDbContext _context;
        public const long tax = 1000;

        public SalaryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(AddSalaryServiceData salaryRawInput, SalaryDataType salaryDataType)
        {
            SalaryParserHandler salaryDataParser = null;

            switch (salaryDataType)
            {
                case SalaryDataType.JSON:
                    // salaryDataParser = new SalaryDataParser(salaryRawInput.Data, new JsonSalaryParser(salaryRawInput.Data));
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
                throw new Exception("salary datatype is not valid");
            }

            var salaryData = salaryDataParser.Parse();
           // var emp_date = salaryData.Select(x => $"{x.EmployeeId}_{x.Date}").ToList();

            // check if any of records exists in salarydata
            //if (_context.Salary.Any(x => emp_date.Contains($"{x.EmployeeId}_{x.Date}")))
            //{
            //    throw new Exception("some data already exists");
            //}


            foreach (var item in salaryData)
            {
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


        }

        public Task Delete(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }

        public Task<SalaryData> Get(long employeeId)
        {
            //var res = _context.Salary.FirstOrDefault(x => x.EmployeeId == employeeId);
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SalaryData>> GetRange(long employeeId)
        {
            throw new NotImplementedException();
        }

        public Task Update(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }
    }
}
