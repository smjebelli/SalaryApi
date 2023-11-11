namespace SalaryApi.DTO.API.Response
{
    public class GetSalaryResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long EmployeeId { get; set; }
        public long BasicSalary { get; set; }
        public long Allowance { get; set; }
        public long Transportation { get; set; }
        public string? Date { get; set; }
        public long ComputedSalary { get; set; }
    }
}
