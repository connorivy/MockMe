namespace MockMe.Exceptions;

public class AssertionFailureException(string? message, Exception? innerException = null)
    : MockMeException(message, innerException) { }
