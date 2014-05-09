namespace RandomExtensions.Randomizers
{
    public sealed class CharRandom : IRandomizeble<char>
    {
        public char Randomize()
        {
            return Randomize(char.MinValue, char.MaxValue);
        }

        public char Randomize(char from, char to)
        {
            return (char) CommonVariables.Instance.Random.Next(from, to + 1);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((char) from, (char) to);
        }

        #endregion
    }
}
