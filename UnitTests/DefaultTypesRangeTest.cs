using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomExtensions;

namespace UnitTests
{
    [TestClass]
    public class DefaultTypesRangeTest
    {
        private static readonly Random Random = new Random(DateTime.UtcNow.Second);

        private static void CheckBounds<T>(T item1, T item2, T item3, dynamic from, dynamic to)
        {
            Assert.IsTrue(from <= item1);
            Assert.IsTrue(from <= item2);
            Assert.IsTrue(from <= item3);

            Assert.IsTrue(to >= item1);
            Assert.IsTrue(to >= item2);
            Assert.IsTrue(to >= item3);
        }

        private static void Testing<T>(T from, T to)
        {
            var item1 = Random.Randomize(from, to);
            var item2 = Random.Randomize(from, to);
            var item3 = Random.Randomize(from, to);
            CheckBounds(item1, item2, item3, from, to);
        }

        [TestMethod]
        public void Decimal()
        {
            var from = -123.123m;
            var to = 567.5675555555555555555m;
            Testing(from, to);
        }

        [TestMethod]
        public void DateTimeType()
        {
            var from = new DateTime(2000, 1, 1);
            var to = new DateTime(2010, 1, 1);
            Testing(from, to);
        }


        [TestMethod]
        public void Char()
        {
            var from = 'a';
            var to = 'h';
            Testing(from, to);
        }
        
        [TestMethod]
        public void Int32()
        {
            var from = 123;
            var to = 567;
            Testing(from, to);
        }

        [TestMethod]
        public void Int64()
        {
            var from = 12300000000000L;
            var to = 56700000000000L;
            Testing(from, to);
        }

        [TestMethod]
        public void UInt64()
        {
            ulong from = 12300000000000000000L;
            ulong to = 16700000000000000000L;
            Testing(from, to);
        }

        [TestMethod]
        public void Bool()
        {
            CheckBool(true, true);
            CheckBool(false, true);
            CheckBool(true, false);
        }

        private void CheckBool(bool hasFalse, bool hasTrue)
        {
            var wasFalse = false;
            var wasTrue = false;
            for (int i = 0; i < 100; i++)
            {
                if (wasFalse && wasTrue)
                {
                    break;
                }

                var item = Random.Randomize(!hasFalse, hasTrue);
                if (item)
                {
                    wasTrue = true;
                }
                else
                {
                    wasFalse = true;
                }
            }
            if (hasFalse)
            {
                Assert.IsTrue(wasFalse);
            }
            else
            {
                Assert.IsFalse(wasFalse);
            }
            if (hasTrue)
            {
                Assert.IsTrue(wasTrue);
            }
            else
            {
                Assert.IsFalse(wasTrue);
            }
        }

        [TestMethod]
        public void Byte()
        {
            byte from = 123;
            byte to = 234;
            Testing(from, to);
        }

        [TestMethod]
        public void SByte()
        {
            sbyte from = -50;
            sbyte to = 50;
            Testing(from, to);
        }

        [TestMethod]
        public void Double()
        {
            var from = 123.123d;
            var to = 567.56000000000000007d;
            Testing(from, to);
        }

        [TestMethod]
        public void Float()
        {
            float from = 123.45F;
            float to = 234.75F;
            Testing(from, to);
        }

        [TestMethod]
        public void UInt32()
        {
            uint from = int.MaxValue + (uint)100;
            uint to = int.MaxValue + (uint)23475;
            Testing(from, to);
        }

        [TestMethod]
        public void Int16()
        {
            short from = 12345;
            short to = 23475;
            Testing(from, to);
        }

        [TestMethod]
        public void UInt16()
        {
            ushort from = 12345;
            ushort to = 23475;
            Testing(from, to);
        }

        [TestMethod]
        public void Enum()
        {
            var from = MyEnum.Two;
            var to = MyEnum.Five;
            Testing(from, to);
        }

        public enum MyEnum
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seveb,
            Eight,
            Nine,
            Ten
        }
    }
}
