using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomExtensions;
using RandomExtensions.Interceptors;
using RandomExtensions.Randomizers;

namespace UnitTests
{
    [TestClass]
    public class ThreadSafeTest
    {
        [TestMethod]
        public void RandomizersThreadSafe()
        {
            var hasExceptions = false;
            for (int i = 0; i < 5000; i++)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.AddRandomizers(
                            new Dictionary<Type, IRandomizeble>
                                {{typeof (int), new Int32Random()}});
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.RemoveRandomizers(new List<Type> {typeof (int)});
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.ClearRandomizers();
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
            }

            Thread.Sleep(2000);
            Assert.IsFalse(hasExceptions);
        }

        [TestMethod]
        public void InterceptorsThreadSafe()
        {
            var hasExceptions = false;
            for (int i = 0; i < 5000; i++)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.AddInterceptors(new List<IInterceptor> {new ArrayInterceptor()});
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.RemoveInterceptors(new List<IInterceptor> { new ArrayInterceptor() });
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        UniversalRandom.ClearInterceptors();
                    }
                    catch (Exception)
                    {
                        hasExceptions = true;
                    }
                });
            }

            Thread.Sleep(2000);
            Assert.IsFalse(hasExceptions);
        }
    }
}
