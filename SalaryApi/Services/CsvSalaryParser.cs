using SalaryApi.Common;
using SalaryApi.DTO.Service;
using System.Text.Json;

namespace SalaryApi.Services
{
    public class CsvSalaryParser : ISalaryDataParser
    {
        protected string _data;
        protected char _delimiter = '/';

        public CsvSalaryParser(string data)
        {
            _data = data;
        }


        public List<SalaryData> Parse()
        {
            throw new NotImplementedException();
        }
    }
}
