using System;
using System.Configuration;

namespace RandomExtensions
{
    public class CommonVariables
    {
        #region Singleton

        private static readonly CommonVariables Singleton = new CommonVariables();

        // ReSharper disable EmptyConstructor
        static CommonVariables()
        // ReSharper restore EmptyConstructor
        {
            //don't mark type as beforefieldinit
        }

        private CommonVariables()
        {
            var randomArrayLength = ConfigurationManager.AppSettings["RandomArrayLength"];
            if (!int.TryParse(randomArrayLength, out _randomArrayLength) || _randomArrayLength < 0)
            {
                _randomArrayLength = 25;
            }
        }

        public static CommonVariables Instance
        {
            get
            {
                return Singleton;
            }
        }

        #endregion Singleton

        #region Variables

        public Random Random = new Random(DateTime.UtcNow.Second);
        private int _randomArrayLength;
        public int RandomArrayLength
        {
            get { return Random.Next(_randomArrayLength); }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value must be greater than zero");
                }

                _randomArrayLength = value;
            }
        }

        #endregion Variables
    }
}
