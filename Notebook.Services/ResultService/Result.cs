namespace Notebook.Services.ResultService;

public sealed class Result
{
    public List<string> ErrorMessages { get; set; } = new List<string>();

    public static Result Success() => new();
    public static Result Error(string errorMessage) => new() { ErrorMessages = [errorMessage] };
    public static Result Error(List<string> errors) => new() { ErrorMessages = errors };

    public void AppendError(string errorMessage) => ErrorMessages.Add(errorMessage);
    public void AppendErrors(List<string> errors) => ErrorMessages = ErrorMessages.Concat(errors).ToList();
}

public sealed class Result<T>
{
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public T Value { get; set; }

    public static Result<T> Success(T value) => new() { Value = value };
    public static Result<T> Error(string errorMessage) => new() { ErrorMessages = [errorMessage] };
    public static Result<T> Error(List<string> errors) => new() { ErrorMessages = errors };

    public void AppendError(string errorMessage) => ErrorMessages.Add(errorMessage);
    public void AppendErrors(List<string> errors) => ErrorMessages = ErrorMessages.Concat(errors).ToList();
}