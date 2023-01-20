using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class OutRecordEntityTypeConfiguration : IEntityTypeConfiguration<OutRecord>
{
    public void Configure(EntityTypeBuilder<OutRecord> builder)
    {
        #region Table

        builder.ToTable(nameof(OutRecord));

        #endregion

        #region Column

        builder.Property(record => record.RowId).HasColumnOrder(0).HasColumnName(nameof(OutRecord.RowId))
            .HasColumnType("INT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(record => record.AccountId).HasColumnOrder(1).HasColumnName(nameof(OutRecord.AccountId))
            .HasColumnType("TINYINT").IsRequired();
        builder.Property(record => record.CategoryId).HasColumnOrder(2).HasColumnName(nameof(OutRecord.CategoryId))
            .HasColumnType("TINYINT").IsRequired();
        builder.Property(record => record.DateTime).HasColumnOrder(3).HasColumnName(nameof(OutRecord.DateTime))
            .HasColumnType("DATETIME").IsRequired();
        builder.Property(record => record.Money).HasColumnOrder(4).HasColumnName(nameof(OutRecord.Money))
            .HasColumnType("FLOAT").IsRequired();
        builder.Property(record => record.Remark).HasColumnOrder(5).HasColumnName(nameof(OutRecord.Remark))
            .HasColumnType("NVARCHAR(300)").IsRequired(false);

        #endregion

        #region Key

        builder.HasKey(record => record.RowId);

        #endregion

        #region Navigation

        builder.HasOne(record => record.Account).WithMany(account => account.OutRecords)
            .HasForeignKey(record => record.AccountId).HasPrincipalKey(account => account.RowId);
        builder.HasOne(record => record.OutCategory).WithMany(category => category.OutRecords)
            .HasForeignKey(record => record.CategoryId).HasPrincipalKey(category => category.RowId);
        builder.HasOne(record => record.TransferContact).WithOne(contact => contact.OutRecord)
            .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}