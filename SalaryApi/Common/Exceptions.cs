namespace SalaryApi.Common
{
    public class InputFormatInvalidException : Exception
    {
        public override string Message => "Input data format is not valid";
    }
}
