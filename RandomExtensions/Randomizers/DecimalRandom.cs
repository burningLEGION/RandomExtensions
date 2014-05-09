using System;

namespace RandomExtensions.Randomizers
{
    public sealed class DecimalRandom : IRandomizeble<decimal>
    {
        public decimal Randomize()
        {
            return new decimal(CommonVariables.Instance.Random.Next(), CommonVariables.Instance.Random.Next(),
                               CommonVariables.Instance.Random.Next(), CommonVariables.Instance.Random.Next(2) == 0,
                               (byte) CommonVariables.Instance.Random.Next(29));
        }

        public decimal Randomize(decimal from, decimal to)
        {
            var fromScale = (byte) (decimal.GetBits(from)[3] >> 16);
            var toScale = (byte) (decimal.GetBits(to)[3] >> 16);
            var scale = (byte) (fromScale + toScale);
            if (scale > 28)
            {
                scale = 28;
            }

            var r = new decimal(CommonVariables.Instance.Random.Next(), CommonVariables.Instance.Random.Next(),
                                    CommonVariables.Instance.Random.Next(), false, scale);
            if (Math.Sign(from) == Math.Sign(to) || from == 0 || to == 0)
            {
                return decimal.Remainder(r, to - from) + from;
            }

            var getFromNegativeRange = (double) from +
                                        CommonVariables.Instance.Random.NextDouble()*((double) to - (double) from) < 0;
            return getFromNegativeRange ? decimal.Remainder(r, -from) + from : decimal.Remainder(r, to);
        }

        #region Implementation of IRandomizeble

        public object RandomizeObject()
        {
            return Randomize();
        }

        public object RandomizeObject(object from, object to)
        {
            return Randomize((decimal) from, (decimal) to)
            ;
        }

        #endregion
    }
}
