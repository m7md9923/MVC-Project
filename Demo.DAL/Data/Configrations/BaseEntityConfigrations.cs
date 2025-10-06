using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Configrations;

public class BaseEntityConfigrations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("GetDate()");
        builder.Property(x=> x.ModifiedOn ).HasComputedColumnSql("GetDate()");
    }
}