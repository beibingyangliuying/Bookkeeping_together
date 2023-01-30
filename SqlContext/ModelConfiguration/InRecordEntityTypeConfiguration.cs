using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.Model;

namespace SqlContext.ModelConfiguration;

internal sealed class InRecordEntityTypeConfiguration : IEntityTypeConfiguration<InRecord>
{
    public void Configure(EntityTypeBuilder<InRecord> builder)
    {
        #region Table

        builder.ToTable(nameof(InRecord));

        #endregion

        #region Column

        builder.Property(record => record.RowId).HasColumnOrder(0).HasColumnName(nameof(InRecord.RowId))
            .HasColumnType("INT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(record => record.AccountId).HasColumnOrder(1).HasColumnName(nameof(InRecord.AccountId))
            .HasColumnType("TINYINT").IsRequired();
        builder.Property(record => record.CategoryId).HasColumnOrder(2).HasColumnName(nameof(InRecord.CategoryId))
            .HasColumnType("TINYINT").IsRequired();
        builder.Property(record => record.DateTime).HasColumnOrder(3).HasColumnName(nameof(InRecord.DateTime))
            .HasColumnType("DATETIME").IsRequired();
        builder.Property(record => record.Money).HasColumnOrder(4).HasColumnName(nameof(InRecord.Money))
            .HasColumnType("FLOAT").IsRequired();
        builder.Property(record => record.Remark).HasColumnOrder(5).HasColumnName(nameof(InRecord.Remark))
            .HasColumnType("NVARCHAR(300)").IsRequired(false);

        #endregion

        #region Key

        builder.HasKey(record => record.RowId);

        #endregion

        #region Index

        builder.HasIndex(record => record.DateTime);
        builder.HasIndex(record => record.Money);

        #endregion

        #region Navigation

        builder.HasOne(record => record.Account).WithMany(account => account.InRecords)
            .HasForeignKey(record => record.AccountId).HasPrincipalKey(account => account.RowId);
        builder.HasOne(record => record.InCategory).WithMany(category => category.InRecords)
            .HasForeignKey(record => record.CategoryId).HasPrincipalKey(category => category.RowId);
        builder.HasOne(record => record.TransferContact).WithOne(contact => contact.InRecord)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}