namespace Model.Classes.App.Result;

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(string message) : base(true, message, default!)
    {
    }

    public SuccessResult(T data) : base(true, data)
    {
    }

    public SuccessResult(T data, string message) : base(true, message, data)
    {
    }
}