using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlContext.ModelClass;

namespace SqlContext.ModelConfigureClass;

public class TransferContactEntityTypeConfiguration : IEntityTypeConfiguration<TransferContact>
{
    public void Configure(EntityTypeBuilder<TransferContact> builder)
    {
        #region Table

        builder.ToTable(nameof(TransferContact));

        #endregion

        #region Column

        builder.Property(contact => contact.RowId).HasColumnOrder(0).HasColumnName(nameof(TransferContact.RowId))
            .HasColumnType("INT").IsRequired().ValueGeneratedOnAdd();
        builder.Property(contact => contact.InRecordId).HasColumnOrder(1)
            .HasColumnName(nameof(TransferContact.InRecordId)).HasColumnType("INT").IsRequired();
        builder.Property(contact => contact.OutRecordId).HasColumnOrder(2)
            .HasColumnName(nameof(TransferContact.OutRecordId)).HasColumnType("INT").IsRequired();

        #endregion

        #region Key

        builder.HasKey(contact => contact.RowId);

        #endregion

        #region Navigation

        builder.HasOne(contact => contact.InRecord).WithOne(record => record.TransferContact)
            .HasForeignKey<TransferContact>(contact => contact.InRecordId)
            .HasPrincipalKey<InRecord>(record => record.RowId);
        builder.HasOne(contact => contact.OutRecord).WithOne(record => record.TransferContact)
            .HasForeignKey<TransferContact>(contact => contact.OutRecordId)
            .HasPrincipalKey<OutRecord>(record => record.RowId);

        #endregion
    }
}