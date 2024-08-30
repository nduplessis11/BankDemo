namespace CurrentAccount.Api.Shared;

public class Maybe<T>
{
    public T? Value { get; private set; }

    public bool HasValue { get; } = false;

    private Maybe(T value)
    {
        Value = value;
        HasValue = true;
    }

    private Maybe() => HasValue = false;

    public static Maybe<T> Some(T value) => new(value);

    public static Maybe<T> None() => new();

    public Maybe<U> Map<U>(Func<T, U> transform)
    {
        if (HasValue) return Maybe<U>.Some(transform(Value!));
        else return Maybe<U>.None();
    }

    public Maybe<U> Bind<U>(Func<T, Maybe<U>> transform)
    {
        if (HasValue) return transform(Value!);
        else return Maybe<U>.None();
    }
}