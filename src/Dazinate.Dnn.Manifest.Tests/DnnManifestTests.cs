using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Autofac;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.Package;
using Xunit;

namespace Dazinate.Dnn.Manifest.Tests
{
    [Collection("Csla")]
    public class DnnManifestTests : BaseBusinessTest, IDisposable
    {

        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public DnnManifestTests()
        {
            Console.Write("initialising");
        }

        private string LoadManifestXml(string localFileName)
        {
            var dir = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, localFileName);
            var xmlContents = File.ReadAllText(filePath);
            return xmlContents;
        }


        [Fact]
        public void Cannot_Load_From_Invalid_Xml()
        {

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
        public void Can_Load_From_Valid_Xml(string manifestFile)
        {

            var xmlContents = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(xmlContents);

            // Assert.
            Assert.NotNull(dnnManifest);
            Assert.False(dnnManifest.IsNew);

            Assert.Equal(dnnManifest.Type, ManifestType.Package);
            Assert.Equal(dnnManifest.Version, "6.0");

            Assert.True(dnnManifest.IsValid);
            Assert.NotNull(dnnManifest.Packages);
            var packages = dnnManifest.Packages;
            Assert.True(packages.Count > 0);

            var firstPackage = packages.First();
            var dependencies = firstPackage.Dependencies;
            Assert.True(dependencies.Count > 0);
            // todo: extend this test to validate object state against xml.

            Assert.False(dnnManifest.IsDirty);

            Assert.NotEmpty(packages);
        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Load_Edit_And_Save(string manifestFile)
        {

            var xmlContents = LoadManifestXml(manifestFile);
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();

            // Act           
            var dnnManifest = factory.Get(xmlContents);
            dnnManifest.Packages[0].Description = "changed";
            dnnManifest.Version = "7.0";

            // var writer = dnnManifest.GetXmlWriter();
            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
                Console.Write(xmlStringBuilder.ToString());
            }

            Assert.False(dnnManifest.IsNew);
            Assert.False(dnnManifest.IsSavable);
            Assert.False(dnnManifest.IsDirty);
        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Manifest_Version_Required(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            dnnManifest.Version = "";


            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);


            var brokenRules = dnnManifest.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(PackagesDnnManifest.VersionProperty);
            Assert.NotNull(rule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Manifest_Version_Must_Be_Valid_Version_String(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            var factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            dnnManifest.Version = "xy.z";

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);

            var brokenRules = dnnManifest.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(PackagesDnnManifest.VersionProperty);
            Assert.NotNull(rule);

            dnnManifest.Version = "1.0.2";
            rule = brokenRules.GetFirstBrokenRule(PackagesDnnManifest.VersionProperty);
            Assert.Null(rule);
        }


        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Icon_File_Only_Allowed_For_Manifests_Version_5_Or_Above(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            // set the manifest version to 6. and add a package with an icon.
            dnnManifest.Version = "6.0";
            var package = dnnManifest.Packages.AddNewPackage();
            package.Name = "somepackage";
            package.FriendlyName = "somefriendly name";
            package.Type = "Module";
            package.Version = "1.0.0";
            package.IconFile = "somefile.png";

            // should be valid.
            Assert.True(dnnManifest.IsValid);

            // change the manifest version to below 5.
            dnnManifest.Version = "4.0";

            // Assert
            // Should now be invalid.
            //  package.CheckRules();
            Assert.False(package.IsValid);
            Assert.False(dnnManifest.IsValid);


            var brokenRules = package.GetBrokenRules();
            var iconRule = brokenRules.GetFirstBrokenRule(Package.IconFileProperty);
            Assert.NotNull(iconRule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Azure_Compatible_Flag_Only_Allowed_For_Manifests_Version_5_Or_Above(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            // set the manifest version to 6. and add a package with an icon.
            dnnManifest.Version = "6.0";
            var package = dnnManifest.Packages.AddNewPackage();
            package.AzureCompatible = true;

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.AzureCompatibleProperty);
            Assert.Null(rule);

            // change the manifest version to below 5.
            dnnManifest.Version = "4.0";

            // Assert
            // Should now be invalid.
            // package.CheckRules();
            Assert.False(package.IsValid);

            brokenRules = package.GetBrokenRules();
            rule = brokenRules.GetFirstBrokenRule(Package.AzureCompatibleProperty);
            Assert.NotNull(rule);

        }


        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Name_Required(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.Name = string.Empty;

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.NameProperty);
            Assert.NotNull(rule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Type_Required(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            // set the manifest version to 6. and add a package with an icon.
            var package = dnnManifest.Packages.AddNewPackage();
            package.Type = string.Empty;

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.TypeProperty);
            Assert.NotNull(rule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Version_Required(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.Version = string.Empty;

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.VersionProperty);
            Assert.NotNull(rule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Version_Must_Be_Valid_Version_String(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.Version = "xy.z";

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.VersionProperty);
            Assert.NotNull(rule);

            package.Version = "1.0.2";
            rule = brokenRules.GetFirstBrokenRule(Package.VersionProperty);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_FriendlyName_Required(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.FriendlyName = string.Empty;

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.FriendlyNameProperty);
            Assert.NotNull(rule);

        }

        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_FriendlyName_Max_Length_250(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.FriendlyName = new string('a', 251);

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.FriendlyNameProperty);
            Assert.NotNull(rule);

            package.FriendlyName = "less than 250";
            rule = brokenRules.GetFirstBrokenRule(Package.FriendlyNameProperty);
            Assert.Null(rule);

        }


        [Theory]
        [InlineData("manifest.xml")]
        public void Validation_Rule_Package_Description_Max_Length_2000(string manifestFile)
        {
            string manifestXml = LoadManifestXml(manifestFile);

            // Act           
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();
            var dnnManifest = factory.Get(manifestXml);

            var package = dnnManifest.Packages.AddNewPackage();
            package.Description = new string('a', 2001);

            // Assert
            // Should now be invalid.
            Assert.False(dnnManifest.IsValid);
            Assert.False(package.IsValid);

            var brokenRules = package.GetBrokenRules();
            var rule = brokenRules.GetFirstBrokenRule(Package.DescriptionProperty);
            Assert.NotNull(rule);

            package.Description = "less than 2000";
            rule = brokenRules.GetFirstBrokenRule(Package.DescriptionProperty);
            Assert.Null(rule);

        }


    }


}


