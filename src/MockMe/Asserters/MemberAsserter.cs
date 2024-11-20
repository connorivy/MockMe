using MockMe.Exceptions;

namespace MockMe.Asserters;

public class MemberAsserter
{
    private readonly int numTimesCalled;

    public MemberAsserter(int numTimesCalled)
    {
        this.numTimesCalled = numTimesCalled;
    }

    public void WasCalled() => this.WasCalled(NumTimes.AtLeast, 1);

    public void WasNotCalled() => this.WasCalled(NumTimes.Exactly, 0);

    public void WasCalled(int numTimesCalled) => this.WasCalled(NumTimes.Exactly, numTimesCalled);

    public void WasCalled(NumTimes intComparison, int numTimesCalled)
    {
        switch (intComparison)
        {
            case NumTimes.Exactly:
                if (this.numTimesCalled != numTimesCalled)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumTimes.AtLeast:
                if (this.numTimesCalled < numTimesCalled)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumTimes.AtMost:
                if (this.numTimesCalled > numTimesCalled)
                {
                    throw new AssertionFailureException();
                }
                break;
            default:
                throw new InvalidOperationException();
        }
    }
}

public enum NumTimes
{
    Exactly,
    AtLeast,
    AtMost,
}
