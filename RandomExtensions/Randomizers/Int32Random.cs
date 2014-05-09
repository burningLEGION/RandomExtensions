namespace RandomExtensions.Randomizers
{
    public sealed class Int32Random : IRandomizeble<int>
    {
        public int Randomize()
        {
            unchecked
            {
                int firstBits = CommonVariables.Instance.Random.Next(0, 1 << 4) << 28;
                int lastBits = CommonVariables.Instance.Random.Next(0, 1 << 28);
                return firstBits | lastBits;
            }
        }

        public int Randomize(int from, int to)
        {
            return CommonVariables.Instance.Random.Next(from, to + 1);//todo
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((int) from, (int) to);
        }

        #endregion
    }
}
