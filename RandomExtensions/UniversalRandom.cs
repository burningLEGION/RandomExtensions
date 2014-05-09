using System;
using System.Collections.Generic;
using System.Linq;

using RandomExtensions.Interceptors;
using RandomExtensions.Randomizers;

namespace RandomExtensions
{
    public static class UniversalRandom
    {
        private static Dictionary<Type, IRandomizeble> _randomizers = new Dictionary<Type, IRandomizeble>();
        private static List<IInterceptor> _interceptors = new List<IInterceptor>();

        static UniversalRandom()
        {
            _randomizers[typeof(decimal)] = new DecimalRandom();
            _randomizers[typeof(string)] = new StringRandom();
            _randomizers[typeof(char)] = new CharRandom();
            _randomizers[typeof(int)] = new Int32Random();
            _randomizers[typeof(long)] = new Int64Random();
            _randomizers[typeof(bool)] = new BoolRandom();
            _randomizers[typeof(byte)] = new ByteRandom();
            _randomizers[typeof(sbyte)] = new SByteRandom();
            _randomizers[typeof(float)] = new FloatRandom();
            _randomizers[typeof(double)] = new DoubleRandom();
            _randomizers[typeof(short)] = new Int16Random();
            _randomizers[typeof(uint)] = new UInt32Random();
            _randomizers[typeof(ushort)] = new UInt16Random();
            _randomizers[typeof(ulong)] = new UInt64Random();
            _randomizers[typeof(DateTime)] = new DateTimeRandom();

            _interceptors.Add(new ArrayInterceptor());
            _interceptors.Add(new EnumInterceptor());
            _interceptors.Add(new ListInterceptor());
        }

        #region Managment

        #region Randomizers

        public static void AddRandomizers(Dictionary<Type, IRandomizeble> randomizers)
        {
            if (randomizers == null)
            {
                throw new ArgumentNullException("randomizers");
            }

            foreach (var keyValuePair in _randomizers)
            {
                if (randomizers.ContainsKey(keyValuePair.Key))
                {
                    continue;
                }

                randomizers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            _randomizers = randomizers;
        }

        public static void RemoveRandomizers(ICollection<Type> types)
        {
            var randomizers = new Dictionary<Type, IRandomizeble>();
            foreach (var keyValuePair in _randomizers)
            {
                if (types.Contains(keyValuePair.Key))
                {
                    continue;
                }

                randomizers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            _randomizers = randomizers;
        }

        public static void ClearRandomizers()
        {
            _randomizers = new Dictionary<Type, IRandomizeble>();
        }

        #endregion Randomizers

        #region Interceptors

        public static void AddInterceptors(List<IInterceptor> interceptors)
        {
            if (interceptors == null)
            {
                throw new ArgumentNullException("interceptors");
            }

            var types = interceptors.Select(x => x.GetType()).ToArray();
            foreach (var interceptor in _interceptors)
            {
                if (types.Contains(interceptor.GetType()))
                {
                    continue;
                }

                interceptors.Add(interceptor);
            }

            _interceptors = interceptors;
        }

        public static void RemoveInterceptors(ICollection<IInterceptor> interceptorsForRemove)
        {
            var interceptors = new List<IInterceptor>();
            var types = interceptorsForRemove.Select(x => x.GetType()).ToArray();
            foreach (var interceptor in _interceptors)
            {
                if (types.Contains(interceptor.GetType()))
                {
                    continue;
                }

                interceptors.Add(interceptor);
            }

            _interceptors = interceptors;
        }

        public static void ClearInterceptors()
        {
            _interceptors = new List<IInterceptor>();
        }

        #endregion Randomizers

        #endregion Managment

        #region Randomize

        public static T Randomize<T>(this Random rnd)
        {
            return Randomize(default(T), default(T), false);
        }

        public static T Randomize<T>(this Random rnd, T from, T to)
        {
            return Randomize(from, to, true);
        }

        private static T Randomize<T>(T from, T to, bool useRange)
        {
            var type = typeof(T);
            var referenceInterceptors = _interceptors;

            foreach (var interceptor in referenceInterceptors)
            {
                if (interceptor.IsMacth(type))
                {
                    return useRange
                               ? (T) interceptor.ExtractRandom(type, from, to)
                               : (T) interceptor.ExtractRandom(type);
                }
            }

            var referenceRandomizers = _randomizers;
            if (!referenceRandomizers.ContainsKey(type))
            {
                return (T)new CustomRandom(type).RandomizeObject();
            }

            var item = referenceRandomizers[type] as IRandomizeble<T>;
            if (item == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            return useRange
                       ? item.Randomize(from, to)
                       : item.Randomize();
        }

        public static object Randomize(this Random rnd, Type type)
        {
            var referenceInterceptors = _interceptors;

            foreach (var interceptor in referenceInterceptors)
            {
                if (interceptor.IsMacth(type))
                {
                    return interceptor.ExtractRandom(type);
                }
            }

            var referenceRandomizers = _randomizers;
            if (!referenceRandomizers.ContainsKey(type))
            {
                return new CustomRandom(type).RandomizeObject();
            }

            return referenceRandomizers[type].RandomizeObject();
        }

        #endregion Randomize
    }
}
