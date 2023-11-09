using SalaryApi.DTO;

namespace SalaryApi.Services
{
    public interface ISalaryService
    {
        Task Add(SalaryData salaryData);
        Task Update(SalaryData salaryData);
        Task Delete(SalaryData salaryData);
        Task<SalaryData> Get(long employeeId);
        Task<IEnumerable<SalaryData>> GetRange(long employeeId);
    }
    public class SalaryService : ISalaryService
    {
        public Task Add(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SalaryData salaryData)
        {
            throw new NotImplementedException();
        }

        public Task<SalaryData> Get(long employeeId)
        {
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
