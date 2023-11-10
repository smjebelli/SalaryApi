namespace SalaryApi.Common
{
    public class BaseResponse
    {
        public string ActionMessage { get; set; }
        public string ActionCode { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
