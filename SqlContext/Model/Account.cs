using SqlContext.Interface;

namespace SqlContext.Model;

public sealed class Account : INamedSeries
{
    public byte RowId { get; set; }
#nullable disable
    public string Name { get; set; }
#nullable enable
    public List<InRecord>? InRecords { get; set; }
    public List<OutRecord>? OutRecords { get; set; }
    public override string ToString() => Name;
}