namespace Model.Classes.App.Result;

public interface IResult<out T>
{
    public string Message { get; }
    public bool Success { get; }
    public T Data { get; }
}