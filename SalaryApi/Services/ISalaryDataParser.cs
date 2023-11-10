using SalaryApi.DTO.Service;

namespace SalaryApi.Services
{
    public interface ISalaryDataParser
    {
        public abstract List<SalaryData> Parse();        
    }
}
