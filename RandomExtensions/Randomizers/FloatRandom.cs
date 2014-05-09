using System;

namespace RandomExtensions.Randomizers
{
    public sealed class FloatRandom : IRandomizeble<float>
    {
        public float Randomize()
        {
            var mantissa = (CommonVariables.Instance.Random.NextDouble() * 2.0) - 1.0;
            var exponent = Math.Pow(2.0, CommonVariables.Instance.Random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        public float Randomize(float from, float to)
        {
            return (float) (from + (CommonVariables.Instance.Random.NextDouble() * (to - from)));
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((float) from, (float) to);
        }

        #endregion
    }
}
