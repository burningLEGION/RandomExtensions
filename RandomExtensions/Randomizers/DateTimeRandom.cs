using System;

namespace RandomExtensions.Randomizers
{
    public sealed class DateTimeRandom : IRandomizeble<DateTime>
    {
        public DateTime Randomize()
        {
            return Randomize(DateTime.MinValue, DateTime.MaxValue);
        }

        public DateTime Randomize(DateTime from, DateTime to)
        {
            return new DateTime(CommonVariables.Instance.Random.Randomize(from.Ticks, to.Ticks));
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((DateTime) from, (DateTime) to);
        }

        #endregion
    }
}
