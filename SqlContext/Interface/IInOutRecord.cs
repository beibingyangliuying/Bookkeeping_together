namespace SqlContext.Interface;

public interface IInOutRecord : IBaseRecord
{
    public byte AccountId { get; set; }
    public byte CategoryId { get; set; }
}