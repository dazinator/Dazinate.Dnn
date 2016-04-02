using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Csla;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Wip;
using Dazinate.Dnn.Manifest.Writer;

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

            using (var manifestFileStream = new XmlTextReader(new StringReader(xml)))
            {
                var instance = Load(manifestFileStream);
                MarkOld(instance);
                CheckRules(instance);
                return instance;
            }
        }

        private PackagesDnnManifest Load(XmlTextReader manifestFileStream)
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
            //   throw new NotImplementedException();
        }

        public PackagesDnnManifest Update(PackagesDnnManifest businessObject)
        {

            // nothing really to do
            foreach (var i in businessObject.Packages)
            {
                MarkOld(i);
                MarkOld(i.License);
                MarkOld(i.Owner);
                if (i.ReleaseNotes != null)
                {
                    MarkOld(i.ReleaseNotes);
                }
            }

            businessObject.Packages.GetDeletedList();
            // var deletedList = businessObject.Packages.GetDeletedList();
            var deletedList = GetDeletedList<IPackage>(businessObject.Packages);
            deletedList.Clear();

            MarkOld(businessObject.Packages);
            MarkOld(businessObject);
            return businessObject;

        }

        public PackagesDnnManifest.SaveToXmlCommand Execute(PackagesDnnManifest.SaveToXmlCommand command)
        {

            var manifest = command.PackagesDnnManifest;

            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                var manifestWriter = new PackagesDnnManifestXmlWriter(xmlWriter);
                manifest.Accept(manifestWriter);
                xmlWriter.Flush();
                command.Xml = xmlStringBuilder.ToString();
            }

            MarkSaved(manifest);
            return command;

        }

        private void MarkSaved(IPackagesDnnManifest manifest)
        {
            // mark the business object as saved
            foreach (var i in manifest.Packages)
            {
                MarkOld(i);
                MarkOld(i.License);
                MarkOld(i.Owner);
                if (i.ReleaseNotes != null)
                {
                    MarkOld(i.ReleaseNotes);
                }
            }

            var deletedList = GetDeletedList<IPackage>(manifest.Packages);
            deletedList.Clear();

            MarkOld(manifest.Packages);
            MarkOld(manifest);
        }
    }
}
