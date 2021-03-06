﻿using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Dependency.ObjectFactory
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

            if (xpathNavigator != null)
            {
                // loop through packages.
                foreach (XPathNavigator dependencyNav in xpathNavigator.Select("dependencies/dependency"))
                {
                    LoadPackageDependency(dependencyNav, list);
                }
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