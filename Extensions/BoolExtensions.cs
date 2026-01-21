namespace CSharpInterviewMessageProcessor.Extensions;

public static class BoolExtensions
{
    /// <summary>
    /// Fluent way to check bool for falsey
    /// </summary>
    public static bool IsFalse(this bool value)
    {
        return value == false;
    }

    /// <summary>
    /// Fluent way to check bool for truthy
    /// </summary>
    public static bool IsTrue(this bool value)
    {
        return value;
    }
}
