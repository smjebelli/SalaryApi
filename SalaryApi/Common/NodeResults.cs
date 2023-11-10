using System.Net;

namespace SalaryApi.Common
{
    public class NodeResult : BaseResult
    {
        public NodeResult(bool operationResult, string message, string code, HttpStatusCode httpStatus) : base(operationResult, message, code, httpStatus)
        {
            this.ActionMessage = message;
            this.ActionCode = code;
            this.HttpStatus = httpStatus;
            this.OperationResult = operationResult;
        }

        public static NodeResult InputSalaryFormatNotValid => new NodeResult(false, "فرمت فایل ورودی معتبر نیست", "1001", HttpStatusCode.BadRequest);
        
    }
}
