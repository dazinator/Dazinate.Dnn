using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class PackageDependenciesListObjectFactory : BaseObjectFactory, IPackageDependenciesListObjectFactory
    {
        private readonly IPackageDependencyObjectFactory _packageDependencyFactory;

        public PackageDependenciesListObjectFactory(IObjectActivator activator, IPackageDependencyObjectFactory packageDependencyFactory) : base(activator)
        {
            _packageDependencyFactory = packageDependencyFactory;
        }

        public PackageDependenciesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<PackageDependenciesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator dependencyNav in xpathNavigator.Select("dependencies/dependency"))
            {
                LoadPackageDependency(dependencyNav, list);
            }

            list.RaiseListChangedEvents = true;

            //var instance = Load(manifestFileStream);
            //MarkOld(instance);
            //CheckRules(instance);
            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadPackageDependency(XPathNavigator nav, PackageDependenciesList list)
        {
            var item = _packageDependencyFactory.Fetch(nav);
            list.Add(item);
        }
        
    }
}