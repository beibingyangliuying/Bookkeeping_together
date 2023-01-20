using System.ComponentModel.DataAnnotations;
using SqlContext.Interface;

namespace SqlContext.ModelClass;

public class InRecord : IInOutRecord
{
    public int RowId { get; set; }
#nullable disable
    public byte AccountId { get; set; }
    public byte CategoryId { get; set; }
#nullable enable
    public DateTime DateTime { get; set; }
    [Range(0, double.MaxValue)] public double Money { get; set; }
    public string? Remark { get; set; }
#nullable disable
    public Account Account { get; set; }
    public InCategory InCategory { get; set; }
    public TransferContact TransferContact { get; set; }
#nullable enable
}