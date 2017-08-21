using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package;
using Dazinate.Dnn.Manifest.Package.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class PackagesDnnManifestObjectFactory : BaseObjectFactory, IPackagesDnnManifestObjectFactory
    {

        private readonly IPackagesListObjectFactory _packagesListFactory;

        public PackagesDnnManifestObjectFactory(IObjectActivator activator, IPackagesListObjectFactory packagesListFactory) : base(activator)
        {
            _packagesListFactory = packagesListFactory;
        }

        public PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents)
        {
            string xml = xmlContents.Value;



            using (var manifestFileStream = XmlReader.Create(new StringReader(xml)))
            {
                var instance = Load(manifestFileStream);
                MarkOld(instance);
                CheckRules(instance);
                return instance;
            }
        }

        private PackagesDnnManifest Load(XmlReader manifestFileStream)
        {

            var dnnManifest = CreateInstance<PackagesDnnManifest>();

            var doc = new XPathDocument(manifestFileStream);

            //Read the root node to determine what version the manifest is
            XPathNavigator rootNav = doc.CreateNavigator();
            rootNav.MoveToFirstChild();

            if (rootNav.Name.ToLower() == "dotnetnuke")
            {
                LoadDotNetNukeManifest(rootNav, dnnManifest);
                //packageType = ReadAttribute(rootNav, "type");
            }
            //else if (rootNav.Name.ToLower() == "languagepack")
            //{
            //    throw new NotSupportedException("This manifest is not in a supported format.");
            //    //LoadLanguagePackManifest(rootNav, dnnManifest);
            //}
            else
            {
                throw new InvalidManifestFormatException();
            }

            return dnnManifest;

        }

        private void LoadDotNetNukeManifest(XPathNavigator rootNav, PackagesDnnManifest dnnPackagesManifest)
        {

            var packageTypeString = XmlUtils.ReadAttribute(rootNav, "type");

            switch (packageTypeString.ToLower())
            {
                case "package":
                    //  InstallerInfo.IsLegacyMode = false;
                    //Parse the package nodes
                    LoadPackageManifest(rootNav, dnnPackagesManifest);
                    break;
                //case "module":
                //case "languagepack":
                //case "skinobject":
                //    rootNav = ConvertLegacyNavigator(rootNav);
                //    LoadPackageManifest(rootNav, dnnManifest);
                //  InstallerInfo.IsLegacyMode = true;
                //break;
                default:
                    throw new InvalidManifestFormatException();
            }

            //throw new NotImplementedException();
        }

        private void LoadPackageManifest(XPathNavigator rootNav, PackagesDnnManifest dnnPackagesManifest)
        {

            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.TypeProperty, ManifestType.Package);
            var version = XmlUtils.ReadRequiredAttribute(rootNav, "version");
            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.VersionProperty, version);
            
            var packagesList = _packagesListFactory.Fetch(rootNav);
            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.PackagesListProperty, packagesList);
        }

        public PackagesDnnManifest.SaveToXmlCommand Execute(PackagesDnnManifest.SaveToXmlCommand command)
        {
            if(command == null)
            {
                throw new System.Exception("command null");
            }
            var manifest = command.PackagesDnnManifest;
            if (manifest == null)
            {
                throw new System.Exception("manifest null");
            }
            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder), new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                var manifestWriter = new SaveToNewXmlFileVisitor(this.Activator, xmlWriter);
                manifest.Accept(manifestWriter);
                xmlWriter.Flush();
                command.Xml = xmlStringBuilder.ToString();
            }

           // MarkSaved(manifest);
            return command;

        }

        public PackagesDnnManifest Create()
        {
            var obj = CreateInstance<PackagesDnnManifest>();
            obj.Packages = _packagesListFactory.Create();
            MarkNew(obj);
            CheckRules(obj);
            return obj;
        }

    }
}
