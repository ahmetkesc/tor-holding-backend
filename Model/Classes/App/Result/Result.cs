namespace Model.Classes.App.Result;

public class Result<T> : IResult<T>
{
    public Result(bool success, string message, T data) : this(success, data)
    {
        Message = message;
        Data = data;
    }

    public Result(bool success, T data)
    {
        Success = success;
        Data = data;
    }

    public Result(T data)
    {
        Data = data;
    }

    public Result(bool success,string message)
    {
        Success = success;
        Message= message;
    }

    public string Message { get; set; } = null!;

    public bool Success { get; set; }

    public T Data { get; set; } = default!;
}