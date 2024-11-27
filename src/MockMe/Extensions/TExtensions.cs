using System.Runtime.CompilerServices;

namespace MockMe.Extensions;

internal static class TExtensions
{
    public static T NotNull<T>(
        this T? value,
        [CallerMemberName] string? callerName = null,
        [CallerLineNumber] int lineNumber = 0
    )
    {
        return value
            ?? throw new Exceptions.NullReferenceException(
                $"Unexpected null value in method {callerName} at line {lineNumber}"
            );
    }
}
