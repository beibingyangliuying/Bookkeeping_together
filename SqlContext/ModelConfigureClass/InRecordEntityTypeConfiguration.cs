using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class InRecordEntityTypeConfiguration : IEntityTypeConfiguration<InRecord>
{
    public void Configure(EntityTypeBuilder<InRecord> builder)
    {
        #region Table

        builder.ToTable("InRecord");

        #endregion

        #region Column

        builder.Property(record => record.RowId).HasColumnOrder(0).HasColumnName("row_id").HasColumnType("BIGINT")
            .IsRequired().ValueGeneratedOnAddOrUpdate();
        builder.Property(record => record.AccountName).HasColumnOrder(1).HasColumnName("account")
            .HasColumnType("NVARCHAR(20)").IsRequired();
        builder.Property(record => record.CategoryName).HasColumnOrder(2).HasColumnName("category")
            .HasColumnType("NVARCHAR(20)").IsRequired();
        builder.Property(record => record.DateTime).HasColumnOrder(3).HasColumnName("datetime")
            .HasColumnType("DATETIME").IsRequired();
        builder.Property(record => record.Money).HasColumnOrder(4).HasColumnName("money").HasColumnType("FLOAT")
            .IsRequired();
        builder.Property(record => record.Remark).HasColumnOrder(5).HasColumnName("remark")
            .HasColumnType("NVARCHAR(300)").IsRequired(false);

        #endregion

        #region Key

        builder.HasKey(record => record.RowId).HasName("PK_ulong_RowId");

        #endregion

        #region Navigation

        builder.HasOne(record => record.Account).WithMany(account => account.InRecords)
            .HasForeignKey(record => record.AccountName).HasConstraintName("FK_InRecord_Account_Account")
            .HasPrincipalKey(account => account.Name);
        builder.HasOne(record => record.InCategory).WithMany(category => category.InRecords)
            .HasForeignKey(record => record.CategoryName).HasConstraintName("FK_InRecord_InCategory_InCategory")
            .HasPrincipalKey(category => category.Name);

        #endregion

        #region Check

        #endregion
    }
}