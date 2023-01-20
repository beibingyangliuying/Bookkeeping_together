namespace SqlContext.ModelClass;

public class TransferContact
{
    public int RowId { get; set; }
    public int InRecordId { get; set; }
    public int OutRecordId { get; set; }
#nullable disable
    public InRecord InRecord { get; set; }
    public OutRecord OutRecord { get; set; }
#nullable enable
}