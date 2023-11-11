using SalaryApi.Common;
using SalaryApi.DTO.Service;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SalaryApi.Services
{
    public class CustomSalaryParser : ISalaryDataParser
    {
        protected string _data;
        private char _delimiter = '/';

        public CustomSalaryParser(string data)
        {
            _data = data;
        }

        public virtual List<SalaryData> Parse()
        {
            List<SalaryData> salaries = new List<SalaryData>();

            using (StringReader reader = new StringReader(_data))
            {
                string firstLine = reader.ReadLine();

                if (!IsFirstLineValidFormat(firstLine, _delimiter))
                    throw new InputFormatInvalidException();

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

        private bool IsFirstLineValidFormat(string firstLine, char delimiter)
        {
            if (firstLine.Contains(delimiter))
            {
                var firstLineArray = firstLine.Split(delimiter);
                if (firstLineArray.Length != 7)
                {
                    return false;
                }
                if (
                    firstLineArray[0].ToUpper() == "EMPLOYEEID" &&
                    firstLineArray[1].ToUpper() == "FIRSTNAME" &&
                    firstLineArray[2].ToUpper() == "LASTNAME" &&
                    firstLineArray[3].ToUpper() == "BASICSALARY" &&
                    firstLineArray[4].ToUpper() == "ALLOWANCE" &&
                    firstLineArray[5].ToUpper() == "TRANSPORTATION" &&
                    firstLineArray[6].ToUpper() == "DATE"
                    )
                {
                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }

        protected bool IsLineDataValid(string[] lineArray, out long employeeId, out long basicSalary, out long allowance, out long transportation)
        {
            employeeId = 0;
            basicSalary = 0;
            allowance = 0;
            transportation = 0;

            if (!long.TryParse(lineArray[0], out employeeId) ||
                !long.TryParse(lineArray[3], out basicSalary) ||
                !long.TryParse(lineArray[4], out allowance) ||
                !long.TryParse(lineArray[5], out transportation))
                return false;
            else
                return true;

        }

    }



}
