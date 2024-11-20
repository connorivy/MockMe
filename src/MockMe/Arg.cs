namespace MockMe;

public class Arg<T>
{
    private readonly bool wasValueSet;
    private readonly T? value;
    private readonly Func<T?, bool>? predicate;

    public static Arg<T> Any { get; } = new(_ => true);

    public Arg(Func<T?, bool> predicate)
    {
        this.wasValueSet = false;
        this.predicate = predicate;
    }

    public Arg(T? value)
    {
        this.wasValueSet = true;
        this.value = value;
    }

    public bool IsSatisfiedBy(T? other)
    {
        if (this.value is not null && this.wasValueSet)
        {
            return this.value.Equals(other);
        }

        if (this.predicate is not null && !this.wasValueSet)
        {
            return this.predicate(other);
        }

        throw new InvalidOperationException("The value and predicate should never both be null");
        //throw new System.Diagnostics.UnreachableException("The value and predicate should never both be null");
    }

    public static implicit operator Arg<T>(T value) => new(value);

    public static implicit operator Arg<T>(Func<T?, bool> value) => new(value);

    public static implicit operator Arg<T>(AnyArg _) => Any;
}

public static class Arg
{
    //private Arg() { }

    public static AnyArg Any { get; } = new AnyArg();

    public static Arg<T> AnyOf<T>() => Arg<T>.Any;

    public static Arg<T> Where<T>(Func<T?, bool> predicate) => new(predicate);
}

public class AnyArg
{
    internal AnyArg() { }
}
