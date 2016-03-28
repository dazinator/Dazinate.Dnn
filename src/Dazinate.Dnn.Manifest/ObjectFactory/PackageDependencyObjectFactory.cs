using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class PackageDependencyObjectFactory : BaseObjectFactory, IPackageDependencyObjectFactory
    {

        public PackageDependencyObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IPackageDependency Fetch(XPathNavigator packageNav)
        {
            // Create the correct concrete dependency based on the xml.

            throw new NotImplementedException();

            //var package = CreateInstance<Package>();
            ////  package.Manifest = dnnPackagesManifest; // to set the parent on each child so a child business rule can access parents properties.

            //LoadProperty(package, Package.NameProperty, XmlUtils.ReadRequiredAttribute(packageNav, "name"));
            //LoadProperty(package, Package.TypeProperty, XmlUtils.ReadRequiredAttribute(packageNav, "type"));
            //LoadProperty(package, Package.VersionProperty, XmlUtils.ReadRequiredAttribute(packageNav, "version"));

            //LoadProperty(package, Package.FriendlyNameProperty, XmlUtils.ReadElement(packageNav, "friendlyName"));
            //LoadProperty(package, Package.DescriptionProperty, XmlUtils.ReadElement(packageNav, "description"));
            //LoadProperty(package, Package.IconFileProperty, XmlUtils.ReadElement(packageNav, "iconFile"));

            //var ownerNode = packageNav.SelectSingleNode("owner");
            //IOwner owner = GetOwner(ownerNode);
            //LoadProperty(package, Package.OwnerProperty, owner);

            //var licenseNode = packageNav.SelectSingleNode("license");
            //ILicense license = GetLicense(licenseNode);
            //LoadProperty(package, Package.LicenseProperty, license);


            ////todo: release notes
            //var releaseNotesNode = packageNav.SelectSingleNode("releaseNotes");
            //IReleaseNotes releaseNotes = GetReleaseNotes(releaseNotesNode);
            //LoadProperty(package, Package.ReleaseNotesProperty, releaseNotes);


            //var azureCompatString = XmlUtils.ReadElement(packageNav, "azureCompatible");
            //if (!string.IsNullOrWhiteSpace(azureCompatString))
            //{
            //    bool azureCompat = bool.Parse(azureCompatString);
            //    LoadProperty(package, Package.AzureCompatibleProperty, azureCompat);
            //}

            //MarkAsChild(package);
            //MarkOld(package);
            //CheckRules(package);

            ////todo: dependencies
            //return package;

        }

       

    }
}