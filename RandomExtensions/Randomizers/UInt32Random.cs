using System;

namespace RandomExtensions.Randomizers
{
    public sealed class UInt32Random : IRandomizeble<uint>
    {
        public uint Randomize()
        {
            var thirtyBits = (uint)CommonVariables.Instance.Random.Next(1 << 30);
            var twoBits = (uint)CommonVariables.Instance.Random.Next(1 << 2);
            return (thirtyBits << 2) | twoBits;
        }

        public uint Randomize(uint from, uint to)
        {
            ulong uRange = to - from;

            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                CommonVariables.Instance.Random.NextBytes(buf);
                ulongRand = (uint)BitConverter.ToInt32(buf, 0);
            } while (ulongRand > uint.MaxValue - ((uint.MaxValue % uRange) + 1) % uRange);

            return (uint) ((ulongRand % uRange) + from);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((uint) from, (uint) to);
        }

        #endregion
    }
}
