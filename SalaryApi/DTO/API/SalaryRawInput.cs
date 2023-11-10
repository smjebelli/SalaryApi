using System.Security.Principal;

namespace SalaryApi.DTO.API
{
    public class AddSalaryRequest
    {
        public IFormFile Data { get; set; }
        public string OverTimeCaluculator { get; set; }
    }
}
