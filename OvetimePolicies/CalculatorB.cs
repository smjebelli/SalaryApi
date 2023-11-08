using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OvetimePolicies
{
    public class CalculatorB : ISalaryCalculator
    {
        private long _basicSalary;
        private long _allowance;
        public CalculatorB(long basicSalary, long allowance)
        {
            _basicSalary = basicSalary;

            _allowance = allowance;

        }
        public long Calculate() => _basicSalary + _allowance;
        
    }
}
