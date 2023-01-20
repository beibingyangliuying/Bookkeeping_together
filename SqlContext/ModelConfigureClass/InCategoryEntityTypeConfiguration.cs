using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class InCategoryEntityTypeConfiguration : IEntityTypeConfiguration<InCategory>
{
    public void Configure(EntityTypeBuilder<InCategory> builder)
    {
        #region Table

        builder.ToTable(nameof(InCategory));

        #endregion

        #region Column

        builder.Property(category => category.RowId).HasColumnOrder(0).HasColumnName(nameof(InCategory.RowId))
            .HasColumnType("TINYINT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(category => category.Name).HasColumnOrder(1).HasColumnName(nameof(InCategory.Name))
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(category => category.RowId);
        builder.HasAlternateKey(category => category.Name);

        #endregion

        #region Navigation

        builder.HasMany(category => category.InRecords).WithOne(record => record.InCategory)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}