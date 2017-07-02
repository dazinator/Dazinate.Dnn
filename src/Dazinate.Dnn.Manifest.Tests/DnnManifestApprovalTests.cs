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
using Dazinate.Dnn.Manifest.Package.Component.Assembly;
using Dazinate.Dnn.Manifest.Package.Component.AuthenticationSystem;
using Dazinate.Dnn.Manifest.Package.Component.Cleanup;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File;

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
            var xmlContents = System.IO.File.ReadAllText(filePath);
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

            // Add an assembly component with some assemblies listed.
            var assyComponent = authSystemPackage.Components.AddNewComponent<IAssemblyComponent>();
            IAssembly assy = (IAssembly)assyComponent.Assemblies.AddNew();
            assy.Name = "test.dll";
            assy.Path = "bin";
            //  assyComponent.Assemblies.ad

            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }

        [Fact]
        public void Can_Add_AssemblyComponent()
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

            // Add an assembly component with some assemblies listed.
            var assyComponent = authSystemPackage.Components.AddNewComponent<IAssemblyComponent>();
            IAssembly assy = (IAssembly)assyComponent.Assemblies.AddNew();
            assy.Name = "foo.dll";
            assy.Path = "bin";

            IAssembly anotherAssy = (IAssembly)assyComponent.Assemblies.AddNew();
            anotherAssy.Name = "bar.dll";
            anotherAssy.Path = "bin";
            anotherAssy.Version = "1.0.0";
            anotherAssy.Action = AssemblyAction.Install;

            IAssembly anotherAssyUnregister = (IAssembly)assyComponent.Assemblies.AddNew();
            anotherAssyUnregister.Name = "baz.dll";
            anotherAssyUnregister.Path = "bin";
            anotherAssyUnregister.Version = "1.0.0.1";
            anotherAssyUnregister.Action = AssemblyAction.Unregister;


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }

        [Fact]
        public void Can_Add_AuthenticationSystemComponent()
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

            // Add an assembly component with some assemblies listed.
            var authComponent = authSystemPackage.Components.AddNewComponent<IAuthenticationSystemComponent>();
            authComponent.Type = "foo";
            authComponent.LoginControlSource = "/some/login.ascx";
            authComponent.LogoffControlSource = "/some/logoff.ascx";
            authComponent.SettingsControlSource = "/some/settings.ascx";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }

        [Fact]
        public void Can_Add_CleanupComponent()
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

            // Add an assembly component with some assemblies listed.
            var component = authSystemPackage.Components.AddNewComponent<ICleanupComponent>();

            var file = (IFile)component.Files.AddNew();
            file.Name = "foo.txt";
            file.Path = "files";
            file.SourceFileName = "bar.txt";

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


