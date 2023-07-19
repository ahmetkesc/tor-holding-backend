namespace Model.Classes.App.Result;

public class ErrorResult<T> : Result<T>
{
    public ErrorResult(T data) : base(false, data)
    {
    }

    public ErrorResult(T data, string message) : base(false, message, data)
    {
    }

    public ErrorResult(string message) : base(false, message)
    {
    }
}