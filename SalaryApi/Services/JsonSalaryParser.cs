using SalaryApi.DTO.Service;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SalaryApi.Services
{
    public class JsonSalaryParser : ISalaryDataParser
    {
        private string _data;
        public JsonSalaryParser(string data)
        {
            _data = data;
        }
        public List<SalaryData> Parse()
        {
            //var salaryData = JsonSerializer.Deserialize<SalaryData>(data);
            //return salaryData;
            throw new NotImplementedException();
        }

        
    }
}
