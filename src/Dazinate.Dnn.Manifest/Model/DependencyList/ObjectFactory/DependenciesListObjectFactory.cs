using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Dependency.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.DependencyList.ObjectFactory
{
    public class DependenciesListObjectFactory : BaseObjectFactory, IDependenciesListObjectFactory
    {
        private readonly IDependencyObjectFactory _packageDependencyFactory;

        public DependenciesListObjectFactory(IObjectActivator activator, IDependencyObjectFactory packageDependencyFactory) : base(activator)
        {
            _packageDependencyFactory = packageDependencyFactory;
        }

        public DependenciesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<DependenciesList>();
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

        public IDependenciesList Create()
        {
            var list = CreateInstance<DependenciesList>();
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadPackageDependency(XPathNavigator nav, DependenciesList list)
        {
            var item = _packageDependencyFactory.Fetch(nav);
            list.Add(item);
        }

    }
}