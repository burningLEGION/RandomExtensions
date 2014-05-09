namespace RandomExtensions.Randomizers
{
    public sealed class BoolRandom : IRandomizeble<bool>
    {
        public bool Randomize()
        {
            return CommonVariables.Instance.Random.Next(0, 2) == 0;
        }

        bool IRandomizeble<bool>.Randomize(bool from, bool to)
        {
            return from == to ? from : Randomize();
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        object IRandomizeble.RandomizeObject(object from, object to)
        {
            return (bool) from == (bool) to ? from : Randomize();
        }

        #endregion
    }
}
