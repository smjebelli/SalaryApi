using System.ComponentModel.DataAnnotations;

namespace SalaryApi.DTO.API.Request
{
    public class UpdateSalaryRequest 
    {
        [Required]
        public long EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long BasicSalary { get; set; }
        public long Allowance { get; set; }
        public long Transportation { get; set; }
        [Required] 
        public string? Date { get; set; }
        [Required] 
        public string OverTimeCaluculator { get; set; }
    }
}
