using System;

namespace MobileProvider
{
    [Flags]
    public enum ActivityTypes
    {
        Unknown = 0,
        Message,
        Call
    }
}