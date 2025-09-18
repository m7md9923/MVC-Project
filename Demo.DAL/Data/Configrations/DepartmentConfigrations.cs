
namespace Demo.DAL.Data.Configrations;

internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn(10, 10);
        builder.Property(x => x.Name).HasColumnType("varchar(20)");
        builder.Property(x => x.Code).HasColumnType("varchar(20)");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("GetDate()");
        builder.Property(x=> x.ModifiedOn ).HasComputedColumnSql("GetDate()");
    }
}