using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.Model;

namespace SqlContext.ModelConfiguration;

internal sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        #region Table

        builder.ToTable(nameof(Account));

        #endregion

        #region Column

        builder.Property(account => account.RowId).HasColumnOrder(0).HasColumnName(nameof(Account.RowId))
            .HasColumnType("TINYINT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(account => account.Name).HasColumnOrder(1).HasColumnName(nameof(Account.Name))
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(account => account.RowId);
        builder.HasAlternateKey(account => account.Name);

        #endregion

        #region Navigation

        builder.HasMany(account => account.InRecords).WithOne(record => record.Account)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(account => account.OutRecords).WithOne(record => record.Account)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}