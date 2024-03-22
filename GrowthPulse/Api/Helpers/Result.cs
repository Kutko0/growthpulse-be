namespace Api.Helpers;

public class Result<T> : Result
{
    public T? Data { get; set; }

    public new static Result<T> Fail(string error)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new[] { error },
        };
    }

    public new static Result<T> Fail(IEnumerable<string> errors)
    {
        return new Result<T>
        {
            Success = false,
            Errors = errors,
        };
    }

    public new static Result<T> Succeed(T data)
    {
        return new Result<T>()
        {
            Success = true,
            Data = data,
        };
    }
}

public class Result
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public static Result Fail(string error)
    {
        return new Result()
        {
            Success = false,
            Errors = new[] { error }
        };
    }

    public static Result Fail(IEnumerable<string> errors)
    {
        return new Result()
        {
            Success = false,
            Errors = errors
        };
    }

    public static Result Succeed()
    {
        return new Result() { Success = true };
    }
}