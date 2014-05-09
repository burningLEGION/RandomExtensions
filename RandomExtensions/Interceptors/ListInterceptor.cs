using System;
using System.Collections;
using System.Collections.Generic;

namespace RandomExtensions.Interceptors
{
    public class ListInterceptor : IInterceptor
    {
        #region Implementation of IInterceptor

        public bool IsMacth(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>);
        }

        public object ExtractRandom(Type type)
        {
            Type itemType = type.GetGenericArguments()[0];
            var res = type.GetConstructors()[0].Invoke(null) as IList;
            if (res == null)
            {
                return null;
            }
            var count = CommonVariables.Instance.RandomArrayLength;
            for (int i = 0; i < count; i++)
            {
                res.Add(CommonVariables.Instance.Random.Randomize(itemType));
            }

            return res;
        }

        object IInterceptor.ExtractRandom(Type type, object from, object to)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
