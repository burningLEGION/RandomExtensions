using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomExtensions;
using RandomExtensions.Interceptors;
using RandomExtensions.Randomizers;

namespace UnitTests
{
    [TestClass]
    public class UniversalRandomTest
    {
        private readonly ExtendedRandom _random = new ExtendedRandom();
        
        [ClassCleanup]
        public static void Cleanup()
        {
            var randomizersList = new Dictionary<Type, IRandomizeble>();
            randomizersList[typeof(decimal)] = new DecimalRandom();
            randomizersList[typeof(string)] = new StringRandom();
            randomizersList[typeof(char)] = new CharRandom();
            randomizersList[typeof(int)] = new Int32Random();
            randomizersList[typeof(long)] = new Int64Random();
            randomizersList[typeof(bool)] = new BoolRandom();
            randomizersList[typeof(byte)] = new ByteRandom();
            randomizersList[typeof(sbyte)] = new SByteRandom();
            randomizersList[typeof(float)] = new FloatRandom();
            randomizersList[typeof(double)] = new DoubleRandom();
            randomizersList[typeof(short)] = new Int16Random();
            randomizersList[typeof(uint)] = new UInt32Random();
            randomizersList[typeof(ushort)] = new UInt16Random();
            randomizersList[typeof(ulong)] = new UInt64Random();
            randomizersList[typeof(DateTime)] = new DateTimeRandom();
            UniversalRandom.AddRandomizers(randomizersList);

            UniversalRandom.AddInterceptors(new List<IInterceptor>
                                                {
                                                    new ListInterceptor(),
                                                    new EnumInterceptor(),
                                                    new ArrayInterceptor()
                                                });
        }

        [TestMethod]
        public void AddRandomizersTest()
        {
            var randomizers = new Dictionary<Type, IRandomizeble> {{typeof (int), new Int32RandomCustom()}};
            UniversalRandom.AddRandomizers(randomizers);
            var item1 = _random.Randomize<int>();
            var item2 = _random.Randomize<int>();
            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1, Int32RandomCustom.FakeRandom);
        }

        #region private sealed class Int32RandomCustom

        private sealed class Int32RandomCustom : IRandomizeble<int>
        {
            public static readonly int FakeRandom = 500;
            public int Randomize()
            {
                return FakeRandom;
            }

            public int Randomize(int from, int to)
            {
                return FakeRandom;
            }

            #region Implementation of IRandomizeble

            public object RandomizeObject()
            {
                return Randomize();
            }

            public object RandomizeObject(object from, object to)
            {
                return FakeRandom;
            }

            #endregion private sealed class Int32RandomCustom
        }

        #endregion

        [TestMethod]
        public void ClearRandomizersTest()
        {
            UniversalRandom.ClearRandomizers();
            var item1 = _random.Randomize<int>();
            var item2 = _random.Randomize<int>();
            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1, 0);
        }

        [TestMethod]
        public void RemoveRandomizersTest()
        {
            ICollection<Type> types = new[] {typeof (int)};
            UniversalRandom.RemoveRandomizers(types);
            var item1 = _random.Randomize<int>();
            var item2 = _random.Randomize<int>();
            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1, 0);
        }

        [TestMethod]
        public void AddInterceptorsTest()
        {
            var interceptors = new List<IInterceptor> {new FakeInterceptor()};
            UniversalRandom.AddInterceptors(interceptors);
            var item1 = _random.Randomize<int>();
            var item2 = _random.Randomize<int>();
            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1, FakeInterceptor.Fake);
        }

        #region private sealed class FakeInterceptor

        private sealed class FakeInterceptor : IInterceptor
        {
            public static readonly int Fake = 123456;

            #region Implementation of IInterceptor

            public bool IsMacth(Type type)
            {
                return type == typeof(int);
            }

            public object ExtractRandom(Type type)
            {
                return Fake;
            }

            public object ExtractRandom(Type type, object from, object to)
            {
                return Fake;
            }

            #endregion private sealed class FakeInterceptor
        }

        #endregion

        [TestMethod]
        public void ClearInterceptorsTest()
        {
            UniversalRandom.ClearInterceptors();
            var item = _random.Randomize<int[]>();
            Assert.IsNull(item);
        }

        [TestMethod]
        public void RemoveInterceptrsTest()
        {
            ICollection<IInterceptor> types = new[] {new ListInterceptor()};
            UniversalRandom.RemoveInterceptors(types);
            var item = _random.Randomize<int[]>();
            Assert.IsNull(item);
        }
    }
}
