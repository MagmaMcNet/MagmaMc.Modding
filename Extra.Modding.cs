using System;

namespace MagmaMc.Modding.Extras
{
    /// <summary>
    /// Loads Class on Mod Load
    /// Requires AutoLoad
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutoLoadAttribute : Attribute
    { }

    /// <summary>
    /// Used as a way to note that this function should be called when the a event has been called
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CallbackAttribute : Attribute
    { }

    /// <summary>
    /// This function should not be used in production
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [Obsolete("This function should be used in production")]
    public class DebugModeAttribute : Attribute
    { }
}