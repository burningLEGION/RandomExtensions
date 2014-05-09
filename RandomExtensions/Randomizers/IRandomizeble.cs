namespace RandomExtensions.Randomizers
{
    public interface IRandomizeble<T> : IRandomizeble
    {
        T Randomize();
        /// <param name="from">left inclusive bound</param>
        /// <param name="to">right inclusive bound</param>
        T Randomize(T from, T to);
    }

    public interface IRandomizeble
    {
        object RandomizeObject();
        /// <param name="from">left inclusive bound</param>
        /// <param name="to">right inclusive bound</param>
        object RandomizeObject(object from, object to);
    }
}
