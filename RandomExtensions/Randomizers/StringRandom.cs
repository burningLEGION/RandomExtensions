namespace RandomExtensions.Randomizers
{
    public sealed class StringRandom : IRandomizeble<string>
    {
        public string Randomize()
        {
            var count = CommonVariables.Instance.RandomArrayLength;
            var array = new char[count];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = CommonVariables.Instance.Random.Randomize<char>();
            }
            return new string(array);
        }

        string IRandomizeble<string>.Randomize(string from, string to)
        {
            return Randomize();
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        object IRandomizeble.RandomizeObject(object from, object to)
        {
            return Randomize();
        }

        #endregion
    }
}
