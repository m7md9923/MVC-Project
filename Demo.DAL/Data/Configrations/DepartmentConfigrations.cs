
using Demo.DAL.Models.DepartmentModule;

namespace Demo.DAL.Data.Configrations;

internal class DepartmentConfigrations : BaseEntityConfigrations<Department>, IEntityTypeConfiguration<Department>
{
    public new void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn(10, 10);
        builder.Property(x => x.Name).HasColumnType("varchar(20)");
        builder.Property(x => x.Code).HasColumnType("varchar(20)");
        base.Configure(builder);
    }
}