using SqlContext.Interface;

namespace SqlContext.ModelClass;

public class Account : INamedSeries
{
    public byte RowId { get; set; }
#nullable disable
    public string Name { get; set; }
    public List<InRecord> InRecords { get; set; }
    public List<OutRecord> OutRecords { get; set; }
#nullable enable
}