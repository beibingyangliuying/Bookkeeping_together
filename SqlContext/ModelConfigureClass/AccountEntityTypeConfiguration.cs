using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        #region Table

        builder.ToTable("Account");

        #endregion

        #region Column

        builder.Property(account => account.RowId).HasColumnOrder(0).HasColumnName("row_id")
            .HasColumnType("TINYINT").IsRequired().ValueGeneratedOnAddOrUpdate();
        builder.Property(account => account.Name).HasColumnOrder(1).HasColumnName("name")
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(account => account.RowId).HasName("PK_byte_RowId");
        builder.HasAlternateKey(account => account.Name).HasName("AK_string_Name");

        #endregion

        #region Navigation

        builder.HasMany(account => account.InRecords).WithOne(record => record.Account)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(account => account.OutRecords).WithOne(record => record.Account)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}