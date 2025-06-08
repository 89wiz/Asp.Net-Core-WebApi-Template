using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApiTemplate.Context;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApiContext).Assembly);
    }
}
