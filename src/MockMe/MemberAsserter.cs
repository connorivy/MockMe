using MockMe.Exceptions;

namespace MockMe;

public class MemberAsserter(int numTimesCalled)
{
    private readonly int numTimesCalled = numTimesCalled;

    public void WasCalled() => this.WasCalled(NumTimes.AtLeast, 1);

    public void WasNotCalled() => this.WasCalled(NumTimes.Exactly, 0);

    public void WasCalled(int numTimesCalled) => this.WasCalled(NumTimes.Exactly, numTimesCalled);

    public void WasCalled(NumTimes intComparison, int numTimesCalled)
    {
        switch (intComparison)
        {
            case NumTimes.Exactly:
                if (numTimesCalled != this.numTimesCalled)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumTimes.AtLeast:
                if (numTimesCalled < this.numTimesCalled)
                {
                    throw new AssertionFailureException();
                }
                break;
            case NumTimes.AtMost:
                if (numTimesCalled > this.numTimesCalled)
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
    AtMost
}
