using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlContext.ModelClass;
using SqlContext.ModelConfigureClass;

namespace SqlContext.ContextClass;

public sealed class BookkeepingContext : DbContext
{
    private static readonly string ConnectionString;
    private static readonly string Schema;
#nullable disable
    public DbSet<Account> Accounts { get; set; }
    public DbSet<InCategory> InCategories { get; set; }
    public DbSet<InRecord> InRecords { get; set; }
    public DbSet<OutCategory> OutCategories { get; set; }
    public DbSet<OutRecord> OutRecords { get; set; }
    public DbSet<TransferContact> TransferContacts { get; set; }
#nullable enable

    static BookkeepingContext()
    {
        var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsettings.json");
        var config = configurationBuilder.Build();
        ConnectionString = config["DataBase:ConnectionString"] ?? throw new InvalidDataException();
        Schema = config["DataBase:Schema"] ?? throw new InvalidDataException();
    }

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
        modelBuilder.ApplyConfiguration(new TransferContactEntityTypeConfiguration());
    }
}