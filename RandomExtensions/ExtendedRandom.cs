using System;
using System.Collections.Generic;

using RandomExtensions.Interceptors;
using RandomExtensions.Randomizers;

namespace RandomExtensions
{
    public sealed class ExtendedRandom
    {
        public static void AddRandomizers(Dictionary<Type, IRandomizeble> randomizers)
        {
            UniversalRandom.AddRandomizers(randomizers);
        }

        public static void RemoveRandomizers(ICollection<Type> types)
        {
            UniversalRandom.RemoveRandomizers(types);
        }

        public static void ClearRandomizers()
        {
            UniversalRandom.ClearRandomizers();
        }

        public static void AddInterceptors(List<IInterceptor> interceptors)
        {
            UniversalRandom.AddInterceptors(interceptors);
        }

        public static void RemoveInterceptors(ICollection<IInterceptor> interceptorsForRemove)
        {
            UniversalRandom.RemoveInterceptors(interceptorsForRemove);
        }

        public static void ClearInterceptors()
        {
            UniversalRandom.ClearInterceptors();
        }
        
        public T Randomize<T>()
        {
            return CommonVariables.Instance.Random.Randomize<T>();
        }

        public T Randomize<T>(T from, T to)
        {
            return CommonVariables.Instance.Random.Randomize(from, to);
        }

        public object Randomize(Type type)
        {
            return CommonVariables.Instance.Random.Randomize(type);
        }
    }
}
