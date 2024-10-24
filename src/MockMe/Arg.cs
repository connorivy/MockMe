namespace MockMe;

public class Arg<T>
{
    private readonly T? value;
    private readonly Func<T, bool>? predicate;

    public static Arg<T> Any { get; } = new(_ => true);

    public Arg(Func<T, bool> predicate)
    {
        this.predicate = predicate;
    }

    public Arg(T value)
    {
        this.value = value;
    }

    public bool IsSatisfiedBy(T other)
    {
        if (value is not null)
        {
            return value.Equals(other);
        }

        return predicate(other);
    }

    public static implicit operator Arg<T>(T value) => new(value);

    public static implicit operator Arg<T>(Func<T, bool> value) => new(value);

    public static implicit operator Arg<T>(AnyArg _) => Any;
}

public static class Arg
{
    //private Arg() { }

    public static AnyArg Any { get; } = new AnyArg();
}

public class AnyArg
{
    internal AnyArg() { }
}
