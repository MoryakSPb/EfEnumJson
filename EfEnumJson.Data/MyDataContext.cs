using EfEnumJson.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EfEnumJson.Data;

public class MyDataContext(DbContextOptions<MyDataContext> options) : DbContext(options)
{
    public DbSet<MyDataEntity> Entities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyDataEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.OwnsOne(x => x.Record, x =>
            {
                x.ToJson();
            });
        });
    }
}