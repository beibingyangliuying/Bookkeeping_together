using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class InCategoryEntityTypeConfiguration : IEntityTypeConfiguration<InCategory>
{
    public void Configure(EntityTypeBuilder<InCategory> builder)
    {
        #region Table

        builder.ToTable("InCategory");

        #endregion

        #region Column

        builder.Property(category => category.RowId).HasColumnOrder(0).HasColumnName("row_id")
            .HasColumnType("TINYINT").IsRequired().ValueGeneratedOnAddOrUpdate();
        builder.Property(category => category.Name).HasColumnOrder(1).HasColumnName("name")
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(category => category.RowId).HasName("PK_byte_RowId");
        builder.HasAlternateKey(category => category.Name).HasName("AK_string_Name");

        #endregion

        #region Navigation

        builder.HasMany(category => category.InRecords).WithOne(record => record.InCategory)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}