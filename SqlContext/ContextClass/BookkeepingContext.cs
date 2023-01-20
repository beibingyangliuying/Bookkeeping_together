using Microsoft.EntityFrameworkCore;
using SqlContext.ModelClass;
using SqlContext.ModelConfigureClass;

namespace SqlContext.ContextClass;

public sealed class BookkeepingContext : DbContext
{
    private const string ConnectionString =
        @"Server=(localdb)\mssqllocaldb;Database=BookkeepingBeiBingYangLiuYing;Trusted_Connection=True;";

    private const string Schema = @"dbo";
#nullable disable
    public DbSet<Account> Accounts { get; set; }
    public DbSet<InCategory> InCategories { get; set; }
    public DbSet<InRecord> InRecords { get; set; }
    public DbSet<OutCategory> OutCategories { get; set; }
    public DbSet<OutRecord> OutRecords { get; set; }
    public DbSet<TransferContact> TransferContacts { get; set; }
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
        modelBuilder.ApplyConfiguration(new TransferContactEntityTypeConfiguration());
    }

    public static void InitializeDataBase()
    {
        using var context = new BookkeepingContext();
        context.Database.EnsureCreated();

        const string name = $"__{nameof(TransferContact)}__";
        try
        {
            context.InCategories.Single(category => category.Name == name);
        }
        catch (InvalidOperationException exception)
        {
            context.InCategories.Add(new InCategory { Name = name });
        }

        try
        {
            context.OutCategories.Single(category => category.Name == name);
        }
        catch (InvalidOperationException exception)
        {
            context.OutCategories.Add(new OutCategory { Name = name });
        }

        context.SaveChanges();
    }
}