namespace SqlContext.Interface;

public interface IRecord
{
    public int RowId { get; set; }
    public byte AccountId { get; set; }
    public byte CategoryId { get; set; }
    public DateTime DateTime { get; set; }
    public double Money { get; set; }
    public string? Remark { get; set; }
}