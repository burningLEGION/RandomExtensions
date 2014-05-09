using System;

namespace RandomExtensions.Randomizers
{
    public sealed class UInt64Random : IRandomizeble<ulong>
    {
        public ulong Randomize()
        {
            long result = CommonVariables.Instance.Random.Next();
            result <<= 31;
            result |= CommonVariables.Instance.Random.Next();
            result <<= 31;
            result |= CommonVariables.Instance.Random.Next();
            return (ulong)result;
        }

        public ulong Randomize(ulong from, ulong to)
        {
            ulong uRange = to - from;

            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                CommonVariables.Instance.Random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (ulongRand % uRange) + from;
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((ulong) from, (ulong) to);
        }

        #endregion
    }
}
