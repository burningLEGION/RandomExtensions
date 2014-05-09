namespace RandomExtensions.Randomizers
{
    public sealed class ByteRandom : IRandomizeble<byte>
    {
        public byte Randomize()
        {
            return Randomize(byte.MinValue, byte.MaxValue);
        }

        public byte Randomize(byte from, byte to)
        {
            return (byte)CommonVariables.Instance.Random.Next(from, to + 1);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((byte) from, (byte) to);
        }

        #endregion
    }
}
