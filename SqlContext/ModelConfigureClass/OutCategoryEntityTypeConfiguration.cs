using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

internal class OutCategoryEntityTypeConfiguration : IEntityTypeConfiguration<OutCategory>
{
    public void Configure(EntityTypeBuilder<OutCategory> builder)
    {
        #region Table

        builder.ToTable(nameof(OutCategory));

        #endregion

        #region Column

        builder.Property(category => category.RowId).HasColumnOrder(0).HasColumnName(nameof(OutCategory.RowId))
            .HasColumnType("TINYINT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(category => category.Name).HasColumnOrder(1).HasColumnName(nameof(OutCategory.Name))
            .HasColumnType("NVARCHAR(20)").IsRequired();

        #endregion

        #region Key

        builder.HasKey(category => category.RowId);
        builder.HasAlternateKey(category => category.Name);

        #endregion

        #region Navigation

        builder.HasMany(category => category.OutRecords).WithOne(record => record.OutCategory)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}