using System.ComponentModel.DataAnnotations;

namespace SalaryApi.DTO.API.Request
{
    public class GetSalaryRangeRequest
    {
        [Required]
        public long EmployeeId { get; set; }
        [Required]
        public string DateStart { get; set; }
        [Required]
        public string DateEnd { get; set; }
    }
}
