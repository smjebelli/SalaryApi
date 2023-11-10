using SalaryApi.Common;
using SalaryApi.DTO.Service;

namespace SalaryApi.Services
{
    public class SalaryParserHandler 
    {
        private ISalaryDataParser _parser;
       

        public SalaryParserHandler(ISalaryDataParser parser)
        {
            _parser = parser;
            
        }
        public List<SalaryData> Parse()
        {
            return _parser.Parse();
        }
    }
}
