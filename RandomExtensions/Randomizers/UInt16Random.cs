namespace RandomExtensions.Randomizers
{
    public sealed class UInt16Random : IRandomizeble<ushort>
    {
        public ushort Randomize()
        {
            return Randomize(ushort.MinValue, ushort.MaxValue);
        }

        public ushort Randomize(ushort from, ushort to)
        {
            return (ushort) CommonVariables.Instance.Random.Next(from, to + 1);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((ushort) from, (ushort) to);
        }

        #endregion
    }
}
