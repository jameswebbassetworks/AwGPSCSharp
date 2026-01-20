using System;

namespace CSharpInterviewMessageProcessor.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Convert string version of long value to long.
    /// </summary>
    /// <param name="value">string number</param>
    /// <returns>long value or long.MinValue</returns>
    public static long ToLong(this string value)
    {
        return long.TryParse(value, out var output) 
            ? output 
            : long.MinValue;
    }
    
    
    /// <summary>
    /// Convert string version of date to DateTimeOffset.
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
    /// Convert string version of int value to int.
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
    /// Fluent expression for checking null of whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        this string? input)
    {
        return string.IsNullOrWhiteSpace(input).IsFalse();
    }
}