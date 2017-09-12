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
using Dazinate.Dnn.Manifest.Package.Component.Config;
using Dazinate.Dnn.Manifest.Package.Component.Container;
using Dazinate.Dnn.Manifest.Package.Component.CoreLanguage;
using Dazinate.Dnn.Manifest.Package.Component.DashboardControl;
using Dazinate.Dnn.Manifest.Package.Component.ExtensionLanguage;
using Dazinate.Dnn.Manifest.Package.Component.File;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary;
using Dazinate.Dnn.Manifest.Package.Component.Module;
using Dazinate.Dnn.Manifest.Package.Component.Provider;
using Dazinate.Dnn.Manifest.Package.Component.ResourceFile;
using Dazinate.Dnn.Manifest.Package.Component.Script;
using Dazinate.Dnn.Manifest.Package.Component.Skin;
using Dazinate.Dnn.Manifest.Package.Component.SkinObject;
using Dazinate.Dnn.Manifest.Package.Component.UrlProvider;
using Assent;
using Assent.Reporters.DiffPrograms;

namespace Dazinate.Dnn.Manifest.Tests
{
    [UseReporter(typeof(DiffReporter))]
    [Collection("Csla")]
    public class DnnManifestApprovalTests : BaseBusinessTest, IDisposable
    {
        private Configuration _configuration;
        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public DnnManifestApprovalTests()
        {
            Console.Write("initialising");
            var configuration = new Configuration().UsingExtension("xml");
            var programs = new IDiffProgram[] {
                    new VsCodeDiffProgram()
                }
                .Concat(Assent.Reporters.DiffReporter.DefaultDiffPrograms)
                .ToArray();
            var reporter = new Assent.Reporters.DiffReporter(programs);
            _configuration = configuration.UsingReporter(reporter);
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
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }

            // Now verify the xml looks good.


            this.Assent(xmlStringBuilder.ToString(), _configuration);
            // Approvals.VerifyXml(xmlStringBuilder.ToString());
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
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }

            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
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
            assyComponent.Assemblies.AddNew();
            IAssembly assy = (IAssembly)assyComponent.Assemblies.Last();
            assy.Name = "foo.dll";
            assy.Path = "bin";

            assyComponent.Assemblies.AddNew();
            IAssembly anotherAssy = (IAssembly)assyComponent.Assemblies.Last();
            anotherAssy.Name = "bar.dll";
            anotherAssy.Path = "bin";
            anotherAssy.Version = "1.0.0";
            anotherAssy.Action = AssemblyAction.Install;

            assyComponent.Assemblies.AddNew();
            IAssembly anotherAssyUnregister = (IAssembly)assyComponent.Assemblies.Last();
            anotherAssyUnregister.Name = "baz.dll";
            anotherAssyUnregister.Path = "bin";
            anotherAssyUnregister.Version = "1.0.0.1";
            anotherAssyUnregister.Action = AssemblyAction.Unregister;


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
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
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
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

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "foo.txt";
            file.Path = "files";
            file.SourceFileName = "bar.txt";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ConfigComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IConfigComponent>();
            component.InstallNodes.AddNew();
            var installNode = (INode)component.InstallNodes.Last();
            installNode.Action = NodeAction.Add;
            installNode.Collision = NodeCollision.Overwrite;
            installNode.InnerXml = @"<some key=""foo"">bar</some>";
            installNode.Key = "key";
            installNode.Path = "config/some";

            component.UninstallNodes.AddNew();
            var uninstallNode = (INode)component.UninstallNodes.Last();
            uninstallNode.Action = NodeAction.Remove;
            uninstallNode.Collision = NodeCollision.Ignore;
            uninstallNode.InnerXml = @"<some key=""foo"">bar</some>";
            uninstallNode.Key = "key";
            uninstallNode.Path = "config/some";




            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ContainerComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IContainerComponent>();
            component.BasePath = "somefoleder/path";
            component.ContainerName = "geewhiz";


            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "foo.txt";
            file.Path = "files";
            file.SourceFileName = "bar.txt";

            component.Files.AddNew();
            file = (IFile)component.Files.Last();
            file.Name = "bar.png";
            file.Path = "files";


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_CoreLanguageComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<ICoreLanguageComponent>();
            component.Code = "ca-ES";
            component.DisplayName = "Catala (España)";
            component.Fallback = "en-US";


            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "classic.ascx.ca-es.resx";
            file.Path = "admin\\ControlPanel\\App_LocalResources";


            component.Files.AddNew();
            file = (IFile)component.Files.Last();
            file.Name = "iconbar.ascx.ca-es.resx";
            file.Path = "admin\\ControlPanel\\App_LocalResources";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_DashboardControlComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IDashboardControlComponent>();
            component.Controls.AddNew();
            var control = (IDashboardControl)component.Controls.Last();
            control.ControllerClass = "DotNetNuke.Modules.Dashboard.Components.Server.ServerController";
            control.Key = "Server";
            control.LocalResourcesFile = "/some/foo.resx";
            control.Source = "DesktopModules/Admin/Dashboard/Modules/Server.ascx";
            control.ViewOrder = 2;
            control.IsEnabled = true;


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ExtensionLanguageComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IExtensionLanguageComponent>();
            component.BasePath = "DesktopModules\\AuthenticationServices\\DNN";
            component.Code = "en-US";
            component.Package = "DefaultAuthentication";

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "Login.ascx.resx";
            file.Path = "app_localresources";

            component.Files.AddNew();
            file = (IFile)component.Files.Last();
            file.Name = "Settings.ascx.resx";
            file.Path = "app_localresources";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_FileComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IFileComponent>();
            component.BasePath = "DesktopModules\\MyModule";

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "MyModule.ascx.resx";
            file.Path = "app_localresources";

            component.Files.AddNew();
            file = (IFile)component.Files.Last();

            file.Name = "MyModule.ascx.resx";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_JavascriptFileComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IJavascriptFileComponent>();
            component.LibraryFolderName = "foo";

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "mylib.js";
            file.Path = "scripts";

            component.Files.AddNew();
            file = (IFile)component.Files.Last();
            file.Name = "mylib.dependency.js";
            // file.Path = "scripts";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_JavascriptLibraryComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IJavascriptLibraryComponent>();
            component.CdnPath = "https://cdn.jsdelivr.net/jquery.cookie/1.4.1/jquery.cookie.min.js";
            component.FileName = "jQuery.cookie.js";
            component.LibraryName = "jQuery.cookie";
            component.ObjectName = "jQuery.cookie";
            component.PreferredScriptLocation = "BodyBottom";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ModuleComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IModuleComponent>();

            component.BusinessControllerClass = "Foo.Module.Controller, Foo.Module";
            component.CodeSubDirectory = "bar";
            component.FolderName = "bas";
            component.IsPremium = false;
            component.ModuleName = "My Module";

            component.ModuleDefinitions.AddNew();
            var moduleDefinition = (IModuleDefinition)component.ModuleDefinitions.Last();
            moduleDefinition.DefaultCacheTime = -1;
            moduleDefinition.FriendlyName = "My Module";

            moduleDefinition.ModuleControls.AddNew();
            var moduleControl = (IModuleControl)moduleDefinition.ModuleControls.Last();
            moduleControl.ControlKey = "";
            moduleControl.ControlSource = "DesktopModules/MyModule/Default.ascx";
            moduleControl.ControlTitle = "My module";
            moduleControl.SupportsPartialRendering = false;
            moduleControl.ControlType = "View";
            moduleControl.IconFile = "";
            moduleControl.HelpUrl = "";
            moduleControl.ViewOrder = 0;

            moduleDefinition.ModulePermissions.AddNew();
            var permission = (IModulePermission)moduleDefinition.ModulePermissions.Last();

            permission.Code = "My_Module";
            permission.Key = "MYMODREAD";
            permission.Name = "My Module - Read stuff";

            component.SupportedFeatures.AddNew();
            var feature = (ISupportedFeature)component.SupportedFeatures.Last();
            feature.FeatureType = SupportedFeatureType.Portable;
            component.SupportedFeatures.AddNew();
            feature = (ISupportedFeature)component.SupportedFeatures.Last();
            feature.FeatureType = SupportedFeatureType.Searchable;
            component.SupportedFeatures.AddNew();
            feature = (ISupportedFeature)component.SupportedFeatures.Last();
            feature.FeatureType = SupportedFeatureType.Upgradeable;

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ProviderComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IProviderComponent>();

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ResourceFileComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IResourceFileComponent>();
            component.BasePath = "foo";

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "foo.ascx.resx";
            file.Path = "resources";

            component.Files.AddNew();
            file = (IFile)component.Files.Last();
            file.Name = "bar.resx";
            // file.Path = "scripts";          

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_ScriptComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IScriptComponent>();
            component.BasePath = "foo";


            component.Scripts.AddNew();
            var file = (IScript)component.Scripts.Last();

            file.Type = ScriptType.Install;
            file.Version = "03.01.00";
            file.Name = "03.01.00.sqldataprovider";
            file.Path = "providers\\dataproviders\\sqldataprovider";


            component.Scripts.AddNew();
            file = (IScript)component.Scripts.Last();
            file.Type = ScriptType.UnInstall;
            file.Version = "05.01.01";
            file.Name = "uninstall.sqldataprovider";
            file.Path = "providers\\dataproviders\\sqldataprovider";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }


        [Fact]
        public void Can_Add_SkinComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<ISkinComponent>();
            component.BasePath = "Portals\\_default\\Skins\\MinimalExtropy";
            component.SkinName = "foo skin";

            component.Files.AddNew();
            var file = (IFile)component.Files.Last();
            file.Name = "index 1024.ascx.resx";
            file.Path = "app_localresources";

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_SkinObjectComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<ISkinObjectComponent>();
            component.ControlKey = "TellFriend";
            component.ControlSource = "DesktopModules/YourCompanyNameSkinObjects/TellFriend.ascx";
            component.SupportsPartialRendering = false;


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }

        [Fact]
        public void Can_Add_UrlProviderComponent()
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
            var component = authSystemPackage.Components.AddNewComponent<IUrlProviderComponent>();
            component.DesktopModule = "Social Groups";
            component.Name = "DNN Social Url Extension Provider";
            component.RedirectAllUrls = true;
            component.ReplaceAllUrls = true;
            component.RewriteAllUrls = true;
            component.SettingsControlSource = "DesktopModules/DNN_SocialUrlProvider/Settings.ascx";
            component.Type = "DotNetNuke.Modules.SocialUrlProvider.SocialUrlProvider";


            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8, Indent = true, NewLineHandling = NewLineHandling.Entitize }))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            // Now verify the xml looks good.
            this.Assent(xmlStringBuilder.ToString(), _configuration);
        }


    }


}


