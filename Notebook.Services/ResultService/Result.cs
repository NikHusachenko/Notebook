namespace Notebook.Services.ResultService;

public sealed class Result
{
    public string ErrorMessage { get; set; }

    public static Result Success() => new();
    public static Result Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}

public sealed class Result<T>
{
    public string ErrorMessage { get; set; }
    public T Value { get; set; }

    public static Result<T> Success(T value) => new() { Value = value };
    public static Result<T> Error(string errorMessage) => new() { ErrorMessage = errorMessage };
}