namespace ApiTemplate.Core.Common;

public struct ErrorOr<T> : IErrorOr
    where T : class?
{
    public ErrorMessage? Error { get; set; }
    public T? Value { get; set; }

    public readonly bool HasError => Error is not null;
    public readonly bool HasValue => Value is not null;

    public ErrorOr() { }
    public ErrorOr(T success) => Value = success;
    public ErrorOr(ErrorMessage error) => Error = error;

    public readonly TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ErrorMessage, TResult> onError)
    {
        if (HasError)
        {
            return onError(Error!);
        }

        return onSuccess(Value!);
    }

    public static implicit operator ErrorOr<T>(T success)
    {
        return new ErrorOr<T>(success);
    }

    public static implicit operator ErrorOr<T>(ErrorMessage error)
    {
        return new ErrorOr<T>(error);
    }
}

public interface IErrorOr
{
    public bool HasError { get; }
    public bool HasValue { get; }
    public ErrorMessage? Error { get; set; }
}