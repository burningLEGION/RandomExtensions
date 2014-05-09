namespace RandomExtensions.Randomizers
{
    public sealed class Int16Random : IRandomizeble<short>
    {
        public short Randomize()
        {
            return (short) CommonVariables.Instance.Random.Next();
        }

        public short Randomize(short from, short to)
        {
            return (short)CommonVariables.Instance.Random.Next(from, to + 1);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((short) from, (short) to);
        }

        #endregion
    }
}
