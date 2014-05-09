using System;
using System.Linq;

namespace RandomExtensions.Interceptors
{
    public class EnumInterceptor : IInterceptor
    {
        #region Implementation of IInterceptor

        public bool IsMacth(Type type)
        {
            return type.IsEnum;
        }

        public object ExtractRandom(Type type)
        {
            var values = Enum.GetValues(type);
            return values.GetValue(CommonVariables.Instance.Random.Next(values.Length));
        }

        public object ExtractRandom(Type type, object from, object to)
        {
            var values =
                Enum.GetValues(type).Cast<int>().Where<int>(x => x >= (int) from && x <= (int) to).ToArray();
            return values.ElementAt(CommonVariables.Instance.Random.Next(values.Count()));
        }

        #endregion
    }
}
