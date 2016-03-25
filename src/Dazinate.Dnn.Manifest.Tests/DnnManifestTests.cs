using System;
using System.IO;
using System.Text;
using System.Xml;
using Autofac;
using Dazinate.Dnn.Manifest;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Ioc;
using Xunit;

namespace Dnn.Contrib.Manifest.Tests
{
    public class DnnManifestTests : IDisposable
    {

        private IContainer _container;

        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public DnnManifestTests()
        {
            Console.Write("initialising");
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(AutofacObjectActivator).Assembly);
            // _containerBuilder = builder;


            _container = builder.Build();
            Csla.Server.FactoryDataPortal.FactoryLoader = new AutoFacObjectFactoryLoader(_container);
        }


        [Fact]
        public void Cannot_Fetch_From_Invalid_Xml()
        {
            // Arrange.    
            // Set up fake orgs data on mock db context.
            // var testData = TestData.GetOrganisations().AsQueryable();
            //var mockOrgsDbSet = MockUtils.CreateMockDbSet(testData);
            // _mockDataContext.Setup(a => a.Organisations).Returns(mockOrgsDbSet.Object);
            var xmlContents = "notvalid xml";

            var factory = new PackagesDnnManifestFactory();
            string manifestXml = xmlContents; //todo

            // Act       
            Assert.Throws<Csla.DataPortalException>(() =>
            {
                var dnnManifest = factory.Get(manifestXml);
            });


        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Fetch_From_Valid_Xml(string manifestFile)
        {
            // Arrange.    
            // Set up fake orgs data on mock db context.
            // var testData = TestData.GetOrganisations().AsQueryable();
            //var mockOrgsDbSet = MockUtils.CreateMockDbSet(testData);
            // _mockDataContext.Setup(a => a.Organisations).Returns(mockOrgsDbSet.Object);

            var dir = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, manifestFile);
            var xmlContents = File.ReadAllText(filePath);

            var factory = _container.Resolve<IDnnManifestFactory<IPackagesDnnManifest>>();
           
            string manifestXml = xmlContents; //todo

            // Act           
            var dnnManifest = factory.Get(manifestXml);

            // Assert.
            Assert.NotNull(dnnManifest);
            Assert.False(dnnManifest.IsNew);

            Assert.Equal(dnnManifest.Type, ManifestType.Package);
            Assert.Equal(dnnManifest.Version, "6.0");

            Assert.True(dnnManifest.IsValid);
            Assert.NotNull(dnnManifest.Packages);
            var packages = dnnManifest.Packages;

            Assert.False(dnnManifest.IsDirty);

            Assert.NotEmpty(packages);
        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Write_To_Xml(string manifestFile)
        {
            // Arrange.    
            // Set up fake orgs data on mock db context.
            // var testData = TestData.GetOrganisations().AsQueryable();
            //var mockOrgsDbSet = MockUtils.CreateMockDbSet(testData);
            // _mockDataContext.Setup(a => a.Organisations).Returns(mockOrgsDbSet.Object);

            var dir = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, manifestFile);
            var xmlContents = File.ReadAllText(filePath);

            var factory = new PackagesDnnManifestFactory();
            string manifestXml = xmlContents; //todo

            // Act           
            var dnnManifest = factory.Get(manifestXml);


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
               // var manifestWriter = new PackagesDnnManifestXmlWriter(xmlWriter);
               // dnnManifest.Accept(manifestWriter);
               // xmlWriter.Flush();
              // Console.Write(xmlStringBuilder.ToString());

                //serializer.Serialize(w, _model);
            }

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Load_Edit_And_Save(string manifestFile)
        {

            var dir = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, manifestFile);
            string xmlContents = File.ReadAllText(filePath);

            var factory = _container.Resolve<IDnnManifestFactory<IPackagesDnnManifest>>();

            // Act           
            var dnnManifest = factory.Get(xmlContents);
            dnnManifest.Packages.RemoveAt(0);
            dnnManifest.Version = "7.0";

           // var writer = dnnManifest.GetXmlWriter();
            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (PackagesDnnManifest)dnnManifest.Save();
            }

            Assert.False(dnnManifest.IsNew);
            Assert.False(dnnManifest.IsSavable);
            Assert.False(dnnManifest.IsDirty);
        }


        [Fact]
        public void Can_Fetch_PackageTypeList()
        {

            var factory = _container.Resolve<IPackageTypeListFactory>();

            // Act    
            var packageTypes = factory.Get();

            // Assert.
            Assert.NotNull(packageTypes);

            foreach (PackageType item in Enum.GetValues(typeof(PackageType)))
            {
                //var listItem = new Csla.NameValueListBase<string, string>.NameValuePair(item.ToString(),
                //    Enum.GetName(typeof(PackageType), item));
                Assert.True(packageTypes.ContainsKey(item.ToString()));
                Assert.Equal(packageTypes.GetItemByKey(item.ToString()).Value, Enum.GetName(typeof(PackageType), item));
            }

        }

        /// <summary>
        /// Dispose is called after each individual test.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
            // _mockDataContext = null;
            Csla.Server.FactoryDataPortal.FactoryLoader = null;
            Console.Write("disposed");
        }
    }


}


