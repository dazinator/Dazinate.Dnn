using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Autofac;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Ioc;
using Xunit;

namespace Dazinate.Dnn.Manifest.Tests
{
    [Collection("Csla")]
    public class PackageTypeListTests : BaseBusinessTest, IDisposable
    {

        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public PackageTypeListTests() : base()
        {
            Console.Write("initialising");
            //    Csla.Server.FactoryDataPortal.FactoryLoader = new AutoFacObjectFactoryLoader();
        }


        [Fact]
        public void Can_Fetch_PackageTypeList()
        {

            var factory = new PackageTypeListFactory();

            // Act    
            var packageTypes = factory.Get();

            // Assert.
            Assert.NotNull(packageTypes);

            foreach (PackageType item in Enum.GetValues(typeof(PackageType)))
            {
                Assert.True(packageTypes.ContainsKey(item.ToString()));
                Assert.Equal(packageTypes.GetItemByKey(item.ToString()).Value, Enum.GetName(typeof(PackageType), item));
            }

        }

      
    }

    public class BaseBusinessTest
    {

        private static int _refCount = 0;
        private static object _lock = new object();

        public BaseBusinessTest()
        {
            Interlocked.Increment(ref _refCount);
            lock (_lock)
            {
                if (_refCount == 1)
                {
                    Console.Write("set csla factory loader");
                    Csla.Server.FactoryDataPortal.FactoryLoader = new AutoFacObjectFactoryLoader();
                }
            }
        }

        public void Dispose()
        {
            Interlocked.Decrement(ref _refCount);
            lock (_lock)
            {
                if (_refCount == 0)
                {
                    Console.Write("disposed csla factory loader");
                    Csla.Server.FactoryDataPortal.FactoryLoader = null;
                }
            }
            // _container.Dispose();
            // _mockDataContext = null;
            //  Csla.Server.FactoryDataPortal.FactoryLoader = null;
            Console.Write("disposed");
        }



    }


}


