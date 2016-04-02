using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory
{
    public class DependencyObjectFactory : BaseObjectFactory, IDependencyObjectFactory
    {

        public DependencyObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IDependency Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.

            var dependencyType = XmlUtils.ReadRequiredAttribute(nav, "type").ToLowerInvariant();
            IDependency dep = null;

            switch (dependencyType)
            {
                case "coreversion":
                    dep = GetCoreVersionDependency(nav);
                    break;
                case "managedpackage":
                    dep = GetManagedPackageDependency(nav);
                    break;
                case "package":
                    dep = GetPackageDependency(nav);
                    break;
                case "type":
                    dep = GetTypeDependency(nav);
                    break;
                default:
                    dep = GetCustomDependency(nav);
                    break;
            }

            MarkAsChild(dep);
            MarkOld(dep);
            CheckRules(dep);
            return dep;
        }

        private IDependency GetCustomDependency(XPathNavigator nav)
        {
            var dep = CreateInstance<CustomDependency>();
            var type = XmlUtils.ReadAttribute(nav, "type");
            LoadProperty(dep, CustomDependency.TypeProperty, type);

            var value = nav.Value;
            LoadProperty(dep, CustomDependency.ValueProperty, value);
            return dep;

        }

        private IDependency GetTypeDependency(XPathNavigator nav)
        {
            var dep = CreateInstance<TypeDependency>();
            var typeName = nav.Value;
            LoadProperty(dep, TypeDependency.TypeNameProperty, typeName);
            return dep;
        }

        private IDependency GetPackageDependency(XPathNavigator nav)
        {
            var dep = CreateInstance<PackageDependency>();
            var packageName = nav.Value;
            LoadProperty(dep, PackageDependency.PackageNameProperty, packageName);
            return dep;
        }

        private IDependency GetManagedPackageDependency(XPathNavigator nav)
        {
            var dep = CreateInstance<ManagedPackageDependency>();
            var version = XmlUtils.ReadAttribute(nav, "version");
            LoadProperty(dep, ManagedPackageDependency.VersionProperty, version);
            var packageName = nav.Value;
            LoadProperty(dep, ManagedPackageDependency.PackageNameProperty, packageName);
            return dep;
        }

        private IDependency GetCoreVersionDependency(XPathNavigator nav)
        {
            var dep = CreateInstance<CoreVersionDependency>();
            var version = nav.Value;
            LoadProperty(dep, CoreVersionDependency.VersionProperty, version);
            return dep;
            // var version = XmlUtils.ReadElement(nav, "dependency").ToLowerInvariant();

        }
    }
}