using System.Diagnostics;

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
        if (this.value is not null)
        {
            return this.value.Equals(other);
        }

        if (this.predicate is not null)
        {
            return this.predicate(other);
        }

        throw new UnreachableException("The value and predicate should never both be null");
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
