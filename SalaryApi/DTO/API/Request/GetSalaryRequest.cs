using System.ComponentModel.DataAnnotations;

namespace SalaryApi.DTO.API.Request
{
    public class GetSalaryRequest
    {
        [Required] 
        public long EmployeeId { get; set; }
        [Required] 
        public string Date { get; set; }
    }
}
