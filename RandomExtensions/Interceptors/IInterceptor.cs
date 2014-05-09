using System;

namespace RandomExtensions.Interceptors
{
    public interface IInterceptor
    {
        bool IsMacth(Type type);
        object ExtractRandom(Type type);
        object ExtractRandom(Type type, object from, object to);
    }
}
