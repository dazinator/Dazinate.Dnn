using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class PackagesListObjectFactory: BaseObjectFactory, IPackagesListObjectFactory
    {
        private readonly IPackageObjectFactory _packageObjectFactory;

        public PackagesListObjectFactory(IObjectActivator activator, IPackageObjectFactory packageObjectFactory) : base(activator)
        {
            _packageObjectFactory = packageObjectFactory;
            //_packagesListFactory = packagesListFactory;
        }

        public PackagesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var packagesList = CreateInstance<PackagesList>();
            packagesList.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator packageNav in xpathNavigator.Select("packages/package"))
            {
                LoadPackage(packageNav, packagesList);
            }

            packagesList.RaiseListChangedEvents = true;

            //var instance = Load(manifestFileStream);
            //MarkOld(instance);
            //CheckRules(instance);
            MarkOld(packagesList);
            MarkAsChild(packagesList);
            CheckRules(packagesList);
            return packagesList;
        }


        private void LoadPackage(XPathNavigator packageNav, PackagesList packagesList)
        {
            var package = _packageObjectFactory.Fetch(packageNav);
            packagesList.Add(package);

            //CreateInstance<Package>();
            ////  package.Manifest = dnnPackagesManifest; // to set the parent on each child so a child business rule can access parents properties.

            //MarkOld(package);
            //MarkAsChild(package);

            //LoadProperty(package, Package.NameProperty, XmlUtils.ReadRequiredAttribute(packageNav, "name"));
            //LoadProperty(package, Package.TypeProperty, XmlUtils.ReadRequiredAttribute(packageNav, "type"));
            //LoadProperty(package, Package.VersionProperty, XmlUtils.ReadRequiredAttribute(packageNav, "version"));

            //packagesList.Add(package);

        }
        
    }
}