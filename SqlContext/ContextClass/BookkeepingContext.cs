// #define UnInitialized

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlContext.ModelClass;
using SqlContext.ModelConfigureClass;

namespace SqlContext.ContextClass;

public sealed class BookkeepingContext : DbContext
{
    private static readonly string ConnectionString;
    private static readonly string Schema;
    public const string Placeholder = $"__{nameof(TransferContact)}__";
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

    public static async Task InitializeDataBase()
    {
        await using var context = new BookkeepingContext();
        await context.Database.EnsureCreatedAsync();

#if UnInitialized
        try
        {
            // Name is unique.
            await context.InCategories.SingleAsync(category => category.Name == Placeholder);
        }
        catch (InvalidOperationException)
        {
            await context.InCategories.AddAsync(new InCategory { Name = Placeholder });
        }

        try
        {
            // Name is unique.
            await context.InCategories.SingleAsync(category => category.Name == Placeholder);
        }
        catch (InvalidOperationException)
        {
            await context.InCategories.AddAsync(new InCategory { Name = Placeholder });
        }

        await context.SaveChangesAsync();
#endif
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