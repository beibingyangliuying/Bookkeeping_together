using SqlContext.Interface;

namespace SqlContext.Model;

public sealed class InCategory : INamedSeries
{
    public byte RowId { get; set; }
#nullable disable
    public string Name { get; set; }
#nullable enable
    public List<InRecord>? InRecords { get; set; }
    public override string ToString() => Name;
}