using System;

namespace RandomExtensions.Randomizers
{
    public sealed class Int64Random : IRandomizeble<long>
    {
        public long Randomize()
        {
            long result = CommonVariables.Instance.Random.Next();
            result <<= 31;
            result |= CommonVariables.Instance.Random.Next();
            result <<= 31;
            result |= CommonVariables.Instance.Random.Next();
            return result;
        }

        public long Randomize(long from, long to)
        {
            byte[] buf = new byte[8];
            CommonVariables.Instance.Random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (to - from)) + from);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((long) from, (long) to);
        }

        #endregion
    }
}
