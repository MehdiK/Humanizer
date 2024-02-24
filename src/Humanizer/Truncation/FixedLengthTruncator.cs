﻿namespace Humanizer;

/// <summary>
/// Truncate a string to a fixed length
/// </summary>
class FixedLengthTruncator : ITruncator
{
    [return: NotNullIfNotNull(nameof(value))]
    public string? Truncate(string? value, int length, string? truncationString, TruncateFrom truncateFrom = TruncateFrom.Right)
    {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            if (truncationString == null || truncationString.Length > length)
            {
                return truncateFrom == TruncateFrom.Right
                    ? value[..length]
                    : value[^length..];
            }

            if (truncateFrom == TruncateFrom.Left)
            {
                return value.Length > length
                    ? truncationString + value[(value.Length - length + truncationString.Length)..]
                    : value;
            }

            return value.Length > length
                ? value[..(length - truncationString.Length)] + truncationString
                : value;
        }
}