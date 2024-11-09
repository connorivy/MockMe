namespace MockMe.Generator.Extensions;

internal static class StringExtensions
{
    public static string AddPrefixIfNotEmpty(this string s, string prefix) =>
        AddOnIfNotEmpty(s, prefix);

    public static string AddSuffixIfNotEmpty(this string s, string suffix) =>
        AddOnIfNotEmpty(s, suffix: suffix);

    public static string AddOnIfNotEmpty(
        this string s,
        string? prefix = null,
        string? suffix = null
    )
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        return prefix + s + suffix;
    }
}
