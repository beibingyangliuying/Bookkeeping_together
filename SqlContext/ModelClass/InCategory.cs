using SqlContext.Interface;

namespace SqlContext.ModelClass;

public class InCategory : INamedSeries
{
    public byte RowId { get; set; }
#nullable disable
    public string Name { get; set; }
    public List<InRecord> InRecords { get; set; }
#nullable enable
}