#define UseLog

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlContext.Model;
using SqlContext.ModelConfiguration;
using SqlContext.Serialization;

namespace SqlContext.Context;

public sealed class BookkeepingContext : DbContext
{
    #region StaticField

    private static readonly string ConnectionString;
    private static readonly string Schema;
    public static readonly byte InPlaceholderId;
    public static readonly byte OutPlaceholderId;
    public const string Placeholder = $"__{nameof(TransferContact)}__";

    public static readonly Func<BookkeepingContext, DateTime, DateTime, IEnumerable<DateAmount>>
        QueryMonthIncomesByDay = EF.CompileQuery((BookkeepingContext context, DateTime start, DateTime end) =>
            context.InRecords
                .AsNoTracking()
                .Where(record => record.DateTime.Date >= start &&
                                 record.DateTime.Date <= end &&
                                 record.CategoryId != InPlaceholderId)
                .GroupBy(record => record.DateTime.Date)
                .Select(group => new DateAmount
                {
                    Date = group.Key,
                    Amount = group.Sum(record => record.Money)
                })
        );

    public static readonly Func<BookkeepingContext, DateTime, DateTime, IEnumerable<DateAmount>>
        QueryMonthOutcomesByDay = EF.CompileQuery((BookkeepingContext context, DateTime start, DateTime end) =>
            context.OutRecords
                .AsNoTracking()
                .Where(record => record.DateTime.Date >= start &&
                                 record.DateTime.Date <= end &&
                                 record.CategoryId != OutPlaceholderId)
                .GroupBy(record => record.DateTime.Date)
                .Select(group => new DateAmount
                {
                    Date = group.Key,
                    Amount = group.Sum(record => record.Money)
                })
        );

    #endregion

    #region InstanceField

#if UseLog
    private readonly StreamWriter
        _streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + @"\logs.txt", true);
#endif

    #endregion

    #region InstanceProperty

#nullable disable
    public DbSet<Account> Accounts { get; set; }
    public DbSet<InCategory> InCategories { get; set; }
    public DbSet<InRecord> InRecords { get; set; }
    public DbSet<OutCategory> OutCategories { get; set; }
    public DbSet<OutRecord> OutRecords { get; set; }
    public DbSet<TransferContact> TransferContacts { get; set; }
#nullable enable

    #endregion

    static BookkeepingContext()
    {
        #region Preprocessing

        var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsettings.json");
        var config = configurationBuilder.Build();

        ConnectionString = config["DataBase:ConnectionString"] ?? throw new InvalidDataException();
        Schema = config["DataBase:Schema"] ?? throw new InvalidDataException();

        #endregion

        #region Initialization

        using var context = new BookkeepingContext();
        context.Database.EnsureCreated();

        try
        {
            var inCategory = context.InCategories.Single(category => category.Name == Placeholder);
            InPlaceholderId = inCategory.RowId;
        }
        catch (InvalidOperationException)
        {
            var entityEntry = context.InCategories.Add(new InCategory { Name = Placeholder });
            InPlaceholderId = entityEntry.Entity.RowId;
            context.SaveChanges();
        }

        try
        {
            var outCategory = context.OutCategories.Single(category => category.Name == Placeholder);
            OutPlaceholderId = outCategory.RowId;
        }
        catch (InvalidOperationException)
        {
            var entityEntry = context.OutCategories.Add(new OutCategory { Name = Placeholder });
            OutPlaceholderId = entityEntry.Entity.RowId;
            context.SaveChanges();
        }

        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString).UseValidationCheckConstraints();
#if UseLog
        optionsBuilder.LogTo(_streamWriter.WriteLine);
#endif
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

#if UseLog
    public override void Dispose()
    {
        base.Dispose();
        _streamWriter.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _streamWriter.DisposeAsync();
    }
#endif
}