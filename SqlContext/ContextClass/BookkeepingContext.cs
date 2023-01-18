using Microsoft.EntityFrameworkCore;
using SqlContext.ModelClass;
using SqlContext.ModelConfigureClass;

namespace SqlContext.ContextClass;

public sealed class BookkeepingContext : DbContext
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=Bookkeeping";
    private const string Schema = @"dbo";
#nullable disable
    public DbSet<Account> Accounts { get; set; }
    public DbSet<InCategory> InCategories { get; set; }
    public DbSet<InRecord> InRecords { get; set; }
    public DbSet<OutCategory> OutCategories { get; set; }
    public DbSet<OutRecord> OutRecords { get; set; }
#nullable enable
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString).UseValidationCheckConstraints();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OutCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InRecordEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OutRecordEntityTypeConfiguration());
    }
}