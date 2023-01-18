using System.ComponentModel.DataAnnotations;
using SqlContext.Interface;

namespace SqlContext.ModelClass;

public class InRecord : IInOutRecord
{
    public ulong RowId { get; set; }
#nullable disable
    public string AccountName { get; set; }
    public string CategoryName { get; set; }
#nullable enable
    public DateTime DateTime { get; set; }
    [Range(0, double.MaxValue)] public double Money { get; set; }
    public string? Remark { get; set; }
#nullable disable
    public Account Account { get; set; }
    public InCategory InCategory { get; set; }
#nullable enable
}