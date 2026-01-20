using System;

namespace CSharpInterviewMessageProcessor.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Convert string version of double value to double.
    /// </summary>
    /// <param name="value">string number</param>
    /// <returns>double value or double.MinValue</returns>
    public static double ToDouble(this string value)
    {
        return double.TryParse(value, out var output)
            ? output
            : double.MinValue;
    }


    /// <summary>
    ///     Convert string version of date to DateTimeOffset.
    /// </summary>
    /// <param name="value">string number</param>
    /// <returns>int value or int.MinValue</returns>
    public static DateTimeOffset ToDateTimeOffset(this string value)
    {
        return DateTime.TryParse(value, out var output)
            ? output
            : DateTimeOffset.MinValue;
    }


    /// <summary>
    ///     Convert string version of int value to int.
    /// </summary>
    /// <param name="value">string number</param>
    /// <returns>int value or int.MinValue</returns>
    public static int ToInt(this string? value)
    {
        return int.TryParse(value, out var output)
            ? output
            : int.MinValue;
    }


    /// <summary>
    ///     Fluent expression for checking null of whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        this string? input)
    {
        return string.IsNullOrWhiteSpace(input).IsFalse();
    }
}
