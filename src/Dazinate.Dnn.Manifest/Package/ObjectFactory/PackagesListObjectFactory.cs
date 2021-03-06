﻿using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.ObjectFactory
{
    public class PackagesListObjectFactory : BaseObjectFactory, IPackagesListObjectFactory
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

            if (xpathNavigator != null)
            {
                // loop through packages.
                foreach (XPathNavigator packageNav in xpathNavigator.Select("packages/package"))
                {
                    LoadPackage(packageNav, packagesList);
                }
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

        public IPackagesList Create()
        {
            var list = CreateInstance<PackagesList>();
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadPackage(XPathNavigator packageNav, PackagesList packagesList)
        {
            var package = _packageObjectFactory.Fetch(packageNav);
            packagesList.Add(package);
        }

       
    }
}