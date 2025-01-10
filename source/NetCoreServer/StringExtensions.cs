namespace NetServer;

/// <summary>
/// String extensions utility class.
/// </summary>
public static class StringExtensions
{
    public static string RemoveSuffix(this string self, char toRemove) => string.IsNullOrEmpty(self) ? self : (self.EndsWith(toRemove) ? self.Substring(0, self.Length - 1) : self);
}