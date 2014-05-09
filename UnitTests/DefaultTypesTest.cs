using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomExtensions;

namespace UnitTests
{
    [TestClass]
    public class DefaultTypesTest
    {
        private static readonly Random Random = new Random(DateTime.UtcNow.Second);

        private static Tuple<T, T, T> Randomize<T>()
        {
            var random = new ExtendedRandom();
            var item1 = random.Randomize<T>();

            var item2 = Random.Randomize<T>();
            var item3 = Random.Randomize<T>();
            return new Tuple<T, T, T>(item1, item2, item3);
        }

        [TestMethod]
        public void Decimal()
        {
            var res = Randomize<decimal>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void DateTimeType()
        {
            var res = Randomize<DateTime>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }


        [TestMethod]
        public void Char()
        {
            var res = Randomize<char>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void String()
        {
            var res = Randomize<string>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        } 
        
        [TestMethod]
        public void Int32()
        {
            var res = Randomize<int>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Int64()
        {
            var res = Randomize<long>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void UInt64()
        {
            var res = Randomize<ulong>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Bool()
        {
            var hasFalse = false;
            var hasTrue = false;
            for (int i = 0; i < 100; i++)
            {
                if (hasFalse && hasTrue)
                {
                    break;
                }

                var item = Random.Randomize<bool>();
                if (item)
                {
                    hasTrue = true;
                }
                else
                {
                    hasFalse = true;
                }
            }

            Assert.IsTrue(hasTrue);
            Assert.IsTrue(hasFalse);
        }

        [TestMethod]
        public void Byte()
        {
            var res = Randomize<byte>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void SByte()
        {
            var res = Randomize<sbyte>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Double()
        {
            var res = Randomize<double>();
            Assert.IsTrue(Math.Abs(res.Item1 - res.Item2) > 0.001 || Math.Abs(res.Item2 - res.Item3) > 0.001);
        }

        [TestMethod]
        public void Float()
        {
            var res = Randomize<float>();
            Assert.IsTrue(Math.Abs(res.Item1 - res.Item2) > 0.01 || Math.Abs(res.Item2 - res.Item3) > 0.01);
        }

        [TestMethod]
        public void UInt32()
        {
            var res = Randomize<uint>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }
        
        [TestMethod]
        public void Object()
        {
            var res = Randomize<object>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Int16()
        {
            var res = Randomize<short>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void UInt16()
        {
            var res = Randomize<ushort>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Custom()
        {
            var res = Randomize<CustomClass>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Enum()
        {
            var res = Randomize<MyEnum>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void Enum2()
        {
            var res = Randomize<MyEnumHundred>();
            Assert.IsTrue(res.Item1 != res.Item2 || res.Item2 != res.Item3);
        }

        [TestMethod]
        public void List()
        {
            var res = Randomize<List<int>>();
            CollectionAssert.AreNotEqual(res.Item1, res.Item2);
            CollectionAssert.AreNotEqual(res.Item2, res.Item3);
        }

        [TestMethod]
        public void Array()
        {
            var res = Randomize<int[]>();
            CollectionAssert.AreNotEqual(res.Item1, res.Item2);
            CollectionAssert.AreNotEqual(res.Item2, res.Item3);
        }

        #region Classes and enum

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

        public enum MyEnumHundred
        {
            One = 100,
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

        public sealed class CustomClass
        {
            public MyEnum Enum;
            public int Integer32 { get; set; }
            public string String { get; set; }
            public CustomInnerClass Class;
            public int[] Array;

            public static bool operator ==(CustomClass left, CustomClass right)
            {
                return left.Integer32 == right.Integer32 &&
                       left.String == right.String &&
                       left.Class == right.Class;
            }

            public static bool operator !=(CustomClass left, CustomClass right)
            {
                return !(left == right);
            }

            public bool Equals(CustomClass other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.Enum, Enum) && Equals(other.Class, Class) && other.Integer32 == Integer32 &&
                       Equals(other.String, String);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (CustomClass)) return false;
                return Equals((CustomClass) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = Enum.GetHashCode();
                    result = (result*397) ^ (Class != null ? Class.GetHashCode() : 0);
                    result = (result*397) ^ Integer32;
                    result = (result*397) ^ (String != null ? String.GetHashCode() : 0);
                    return result;
                }
            }
        }

        public sealed class CustomInnerClass
        {
            public long Integer64 { get; set; }
            public decimal Decimal { get; set; }
            public char Char;
            public List<int> List;

            public static bool operator ==(CustomInnerClass left, CustomInnerClass right)
            {
                return left.Integer64 == right.Integer64 &&
                       left.Decimal == right.Decimal &&
                       left.Char == right.Char;
            }

            public static bool operator !=(CustomInnerClass left, CustomInnerClass right)
            {
                return !(left == right);
            }

            public bool Equals(CustomInnerClass other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return other.Char == Char && other.Integer64 == Integer64 && other.Decimal == Decimal;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (CustomInnerClass)) return false;
                return Equals((CustomInnerClass) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = Char.GetHashCode();
                    result = (result*397) ^ Integer64.GetHashCode();
                    result = (result*397) ^ Decimal.GetHashCode();
                    return result;
                }
            }
        }

        #endregion Classes and enum
    }
}
