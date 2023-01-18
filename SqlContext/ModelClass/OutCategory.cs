using SqlContext.Interface;

namespace SqlContext.ModelClass;

public class OutCategory : INamedSeries
{
    public byte RowId { get; set; }
#nullable disable
    public string Name { get; set; }
    public List<OutRecord> OutRecords { get; set; }
#nullable enable
}