using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class OutCategoryEntityTypeConfiguration : IEntityTypeConfiguration<OutCategory>
{
    public void Configure(EntityTypeBuilder<OutCategory> builder)
    {
        #region Table

        builder.ToTable("OutCategory");

        #endregion

        #region Column

        builder.Property(category => category.RowId).HasColumnOrder(0).HasColumnName("row_id").HasColumnType("TINYINT")
            .IsRequired().ValueGeneratedOnAddOrUpdate();
        builder.Property(category => category.Name).HasColumnOrder(1).HasColumnName("name")
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(category => category.RowId).HasName("PK_byte_RowId");
        builder.HasAlternateKey(category => category.Name).HasName("AK_string_Name");

        #endregion

        #region Navigation

        builder.HasMany(category => category.OutRecords).WithOne(record => record.OutCategory)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}