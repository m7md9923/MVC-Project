using Demo.DAL.Models.EmployeeModule;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Configrations;

public class EmployeeConfigration : BaseEntityConfigrations<Employee>, IEntityTypeConfiguration<Employee>
{
    public new void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(E => E.Address).HasColumnType("varchar(50)");
        builder.Property(E => E.Name).HasColumnType("varchar(50)");
        builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
        builder.Property(E => E.Gender).HasConversion(empGender => empGender.ToString(),
            gender => (Gender)Enum.Parse(typeof(Gender), gender));
        
        builder.Property(E => E.EmployeeType).HasConversion(empType => empType.ToString(),
            employeeType => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType));
        
        base.Configure(builder);
        
    }
}
