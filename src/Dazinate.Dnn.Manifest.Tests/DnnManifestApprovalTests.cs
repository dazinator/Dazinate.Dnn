using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Dependency;
using Xunit;

namespace Dazinate.Dnn.Manifest.Tests
{
    [UseReporter(typeof(DiffReporter))]
    [Collection("Csla")]
    public class DnnManifestApprovalTests : BaseBusinessTest, IDisposable
    {

        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public DnnManifestApprovalTests()
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


        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Load_And_Save_Manifest(string manifestFile)
        {
            //  var names = new[] { "Llewellyn", "James", "Dan", "Jason", "Katrina" };
            // Array.Sort(names);
            var xmlContents = LoadManifestXml(manifestFile);
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();

            // Act           
            var dnnManifest = factory.Get(xmlContents);
            dnnManifest.Packages[0].Description = "changed";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }

            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }

        [Fact]
        public void Can_Create_Manifest()
        {

            var factory = new PackagesDnnManifestFactory();
          
            // Act   
            // Create a fully populated package manifest, demonstrating all possible manifest features.        
            var dnnManifest = factory.CreateNewManifest();
            dnnManifest.Version = "5.0";
            dnnManifest.Type = ManifestType.Package;
            
            // Add an Auth_System package.
            var authSystemPackage = dnnManifest.Packages.AddNewPackage();
            authSystemPackage.Name = "MyAuthSystemPackage";
            authSystemPackage.Description = "An amazing auth system.";
            authSystemPackage.FriendlyName = "My Amazing Auth System";
            authSystemPackage.Version = "1.0.0";
            authSystemPackage.Type = "Auth_System";

            // The auth system package has some dependencies.
            var dependency = (CoreVersionDependency)authSystemPackage.Dependencies.AddNewCoreVersionDependency();
            dependency.Version = "5.0";

            var customDependency = (CustomDependency)authSystemPackage.Dependencies.AddNewCustomDependency();
            customDependency.Type = "Custom";
            customDependency.Value = "SpecialValue";

            var managedPackagedDependency = (ManagedPackageDependency)authSystemPackage.Dependencies.AddNewManagedPackageDependency();
            managedPackagedDependency.PackageName = "SomeOtherPackage";
            managedPackagedDependency.Version = "1.0.0";

            var packagedDependency = (PackageDependency)authSystemPackage.Dependencies.AddNewPackageDependency();
            packagedDependency.PackageName = "AndAnotherPackage";

            var typeDependency = (TypeDependency)authSystemPackage.Dependencies.AddNewTypeDependency();
            typeDependency.TypeName = "System.Version";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }

            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }



    }


}


