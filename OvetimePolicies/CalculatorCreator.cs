using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvetimePolicies
{
    public class CalculatorCreator
    {
        private string _calculatorType;
        private long _basicSalary;
        private long _allowance;

        public CalculatorCreator(string calculatorType, long basicSalary, long allowance)
        {
            _calculatorType = calculatorType;   
            _allowance = allowance;
            _basicSalary = basicSalary;
        }
        public ISalaryCalculator CreateCalculator()
        {
            if (_calculatorType == "A")
                return new CalculatorA();
            else if (_calculatorType == "B")
                return new CalculatorB(_basicSalary, _allowance);
            else if (_calculatorType == "C")
                return new CalculatorC();
            else
                return new CalculatorB(_basicSalary, _allowance);


        }
    }
}
