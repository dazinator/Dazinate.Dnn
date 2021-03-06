﻿using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Container.ObjectFactory
{
    public class ContainerFilesListObjectFactory : BaseObjectFactory, IContainerFilesListObjectFactory
    {
        private readonly IContainerFileObjectFactory _fileObjectFactory;

        public ContainerFilesListObjectFactory(IObjectActivator activator, IContainerFileObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IContainerFilesList Create()
        {
            var list = CreateInstance<ContainerFilesList>();
            MarkAsChild(list);
            MarkNew(list);
            return list;
        }

        public IContainerFilesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ContainerFilesList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                // loop through packages.
                foreach (XPathNavigator dependencyNav in xpathNavigator.Select("containerFile"))
                {
                    LoadFileItem(dependencyNav, list);
                }
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IContainerFilesList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}