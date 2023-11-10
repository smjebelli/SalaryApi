using System.Net;

namespace SalaryApi.Common
{
    public class HttpStatusAttribute : Attribute
    {
        public HttpStatusCode HttpStatus { get; }

        public HttpStatusAttribute(HttpStatusCode httpStatus)
        {
            this.HttpStatus = httpStatus;
        }
    }
}
