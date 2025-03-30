namespace DD.NameSorter.Abstractions;

public class Result<T>
{
    public T? Value { get; }
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
        ErrorMessage = null;
    }

    private Result(string errorMessage)
    {
        Value = default;
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string errorMessage) => new(errorMessage);
}