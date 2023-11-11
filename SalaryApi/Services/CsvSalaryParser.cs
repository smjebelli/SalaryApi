using SalaryApi.Common;
using SalaryApi.DTO.Service;
using System.Text.Json;

namespace SalaryApi.Services
{
    public class CsvSalaryParser : CustomSalaryParser
    {
        protected string _data;
        private char _delimiter = ',';

        public CsvSalaryParser(string data):base(data) 
        {
            _data = data;
        }


        public override List<SalaryData> Parse()
        {
            List<SalaryData> salaries = new List<SalaryData>();

            using (StringReader reader = new StringReader(_data))
            {
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    var lineArray = line.Split(_delimiter);
                    long employeeId, basicSalary, allowance, transportation;

                    if (!IsLineDataValid(lineArray, out employeeId, out basicSalary, out allowance, out transportation))
                        throw new InputFormatInvalidException();

                    salaries.Add(new SalaryData()
                    {
                        EmployeeId = employeeId,
                        FirstName = lineArray[1],
                        LastName = lineArray[2],
                        BasicSalary = basicSalary,
                        Transportation = transportation,
                        Allowance = allowance,
                        Date = lineArray[6]
                    });
                }
            }

            return salaries;
        }
    }
}
