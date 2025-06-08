using ApiTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Context.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), schema: "Auth")
            .HasKey(x => x.Id);

        builder.Property(x => x.Username).HasColumnType("varchar(30)");
        builder.Property(x => x.Password).HasColumnType("varchar(250)");
        builder.Property(x => x.Email).HasColumnType("varchar(120)");

        builder.HasIndex(x => x.Username).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
    }
}
