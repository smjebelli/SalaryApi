using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalaryApi.Models;

namespace SalaryApi.Configurations
{
    public class SalaryCofigurations : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).IsRequired();

            builder.HasData(new Salary()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ali",
                LastName = "Ahmadi",
                EmployeeId = 100,
                BasicSalary = 10000000,
                Allowance = 300000,
                Transportation = 500000,
                Date = "14020801"
            });
        }
    }

}
