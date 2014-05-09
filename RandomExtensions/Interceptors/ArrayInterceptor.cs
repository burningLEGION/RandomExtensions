using System;

namespace RandomExtensions.Interceptors
{
    public class ArrayInterceptor : IInterceptor
    {
        #region Implementation of IInterceptor

        public bool IsMacth(Type type)
        {
            return type.BaseType == typeof(Array);
        }

        public object ExtractRandom(Type type)
        {
            var elementType = type.GetElementType();
            var res = Array.CreateInstance(type.GetElementType(), CommonVariables.Instance.RandomArrayLength);
            for (int i = 0; i < res.Length; i++)
            {
                res.SetValue(CommonVariables.Instance.Random.Randomize(elementType), i);
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
