namespace RandomExtensions.Randomizers
{
    public sealed class SByteRandom : IRandomizeble<sbyte>
    {
        public sbyte Randomize()
        {
            return (sbyte)CommonVariables.Instance.Random.Next(-128, 128);
        }

        public sbyte Randomize(sbyte from, sbyte to)
        {
            return (sbyte)CommonVariables.Instance.Random.Next(from, to);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((sbyte)from, (sbyte)to);
        }

        #endregion
    }
}
