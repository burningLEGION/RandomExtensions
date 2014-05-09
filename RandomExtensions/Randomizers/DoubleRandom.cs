using System;

namespace RandomExtensions.Randomizers
{
    public sealed class DoubleRandom : IRandomizeble<double>
    {
        public double Randomize()
        {
            var mantissa = (CommonVariables.Instance.Random.NextDouble() * 2.0) - 1.0;
            var exponent = Math.Pow(2.0, CommonVariables.Instance.Random.Next(-126, 128));
            return mantissa * exponent;
        }

        public double Randomize(double from, double to)
        {
            return from + (CommonVariables.Instance.Random.NextDouble()*(to - from));
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((double) from, (double) to);
        }

        #endregion
    }
}
