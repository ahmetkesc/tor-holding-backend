using Model.Classes.App.Result;

namespace Business.Helpers;

public class Result
{
    public static IResult<T> Ok<T>(T data, string message)
    {
        return new SuccessResult<T>(data, message);
    }

    public static IResult<T> Ok<T>(T data)
    {
        return new SuccessResult<T>(data);
    }

    public static IResult<T> Message<T>(string message)
    {
        return new ErrorResult<T>(message);
    }
}