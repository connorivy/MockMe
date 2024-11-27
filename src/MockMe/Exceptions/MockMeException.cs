namespace MockMe.Exceptions;

public class MockMeException(string? message, Exception? innerException = null)
    : Exception(message, innerException) { }
