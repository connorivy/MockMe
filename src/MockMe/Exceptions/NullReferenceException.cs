namespace MockMe.Exceptions;

public class NullReferenceException(string? message, Exception? innerException = null)
    : Exception(message, innerException) { }
