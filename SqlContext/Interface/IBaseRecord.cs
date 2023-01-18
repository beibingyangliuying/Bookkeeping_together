namespace SqlContext.Interface;

public interface IBaseRecord
{
    public ulong RowId { get; set; }
    public DateTime DateTime { get; set; }
    public double Money { get; set; }
    public string? Remark { get; set; }
}