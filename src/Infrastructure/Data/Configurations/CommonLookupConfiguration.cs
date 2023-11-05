using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
internal class CommonLookupConfiguration : IEntityTypeConfiguration<CommonLookup>
{
    public void Configure(EntityTypeBuilder<CommonLookup> builder)
    {
        builder.Property(t => t.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(t => t.Name)
           .HasMaxLength(50)
           .IsRequired();

        builder.Property(t => t.Description)
           .HasMaxLength(100)
           .IsRequired();

        builder.Property(t => t.TypeCode)
           .HasMaxLength(10)
           .IsRequired();
    }
}
