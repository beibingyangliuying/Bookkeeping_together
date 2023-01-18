namespace SqlContext.Interface;

public interface IInOutRecord : IBaseRecord
{
    public string AccountName { get; set; }
    public string CategoryName { get; set; }
}