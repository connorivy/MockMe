using MockMe.Exceptions;

namespace MockMe.Asserters;

public class MemberAsserter
{
    private readonly int numTimesCalled;

    public MemberAsserter(int numTimesCalled)
    {
        this.numTimesCalled = numTimesCalled;
    }

    public void WasCalled() => this.WasCalled(NumTimes.AtLeast(1));

    public void WasNotCalled() => this.WasCalled(NumTimes.Exactly(0));

    public void WasCalled(int numTimesCalled) => this.WasCalled(NumTimes.Exactly(numTimesCalled));

    public void WasCalled(NumTimes numTimesExpected) =>
        numTimesExpected.AssertSatisfied(this.numTimesCalled);
}

public abstract class NumTimes
{
    public static NumTimes AtLeast(int numTimes) =>
        new SingleNumComparisonNumTimes(NumComparison.AtLeast, numTimes);

    public static NumTimes AtMost(int numTimes) =>
        new SingleNumComparisonNumTimes(NumComparison.AtMost, numTimes);

    public static NumTimes Exactly(int numTimes) =>
        new SingleNumComparisonNumTimes(NumComparison.Exactly, numTimes);

    public static NumTimes Between(int lowestValueExpected, int highestValueExpected) =>
        new BetweenNumTimes(lowestValueExpected, highestValueExpected);

    internal abstract void AssertSatisfied(int numTimesActuallyCalled);
}

internal class SingleNumComparisonNumTimes : NumTimes
{
    private readonly NumComparison numComparison;
    private readonly int expectedNumTimes;

    internal SingleNumComparisonNumTimes(NumComparison numComparison, int expectedNumTimes)
    {
        this.numComparison = numComparison;
        this.expectedNumTimes = expectedNumTimes;
    }

    internal override void AssertSatisfied(int numTimesActuallyCalled)
    {
        switch (this.numComparison)
        {
            case NumComparison.Exactly:
                if (numTimesActuallyCalled != this.expectedNumTimes)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumComparison.AtLeast:
                if (numTimesActuallyCalled < this.expectedNumTimes)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumComparison.AtMost:
                if (numTimesActuallyCalled > this.expectedNumTimes)
                {
                    throw new AssertionFailureException();
                }
                break;
            default:
                throw new InvalidOperationException();
        }
    }
}

internal class BetweenNumTimes : NumTimes
{
    private readonly int lowestPossible;
    private readonly int highestPossible;

    internal BetweenNumTimes(int lowestPossible, int highestPossible)
    {
        this.lowestPossible = lowestPossible;
        this.highestPossible = highestPossible;
    }

    internal override void AssertSatisfied(int numTimesActuallyCalled)
    {
        if (
            this.lowestPossible <= numTimesActuallyCalled
            && numTimesActuallyCalled <= this.highestPossible
        )
        {
            return;
        }

        throw new AssertionFailureException();
    }
}

internal enum NumComparison
{
    Exactly,
    AtLeast,
    AtMost,
}
