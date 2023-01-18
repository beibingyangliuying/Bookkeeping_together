namespace SqlContext.Interface;

public interface INamedSeries
{
    public byte RowId { get; set; }
    public string Name { get; set; }
}