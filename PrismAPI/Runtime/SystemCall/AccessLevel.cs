namespace PrismAPI.Runtime.SystemCall;

public enum AccessLevel
{
    /// <summary>
    /// Full access to all files and features. No access to full system memory.
    /// Core apps are exepmt from external kernel permissions due to them possibly
    /// requiring those permissions, and just the nature in how they work. This
    /// will be acceptable because external kernel access cannot modify internal
    /// kernel accessing apps, so they cannot be injected with malicous code.
    /// </summary>
    Kernel,

    /// <summary>
    /// Basic access to the current active user's home directory and features such as UI, Graphics,
    /// Audio, and others.
    /// </summary>
    User,
}