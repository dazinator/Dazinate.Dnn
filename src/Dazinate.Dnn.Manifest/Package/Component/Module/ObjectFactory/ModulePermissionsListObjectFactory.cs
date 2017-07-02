using System;
using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModulePermissionsListObjectFactory : BaseObjectFactory, IModulePermissionsListObjectFactory
    {
        private readonly IModulePermissionObjectFactory _permissionObjectFactory;

        public ModulePermissionsListObjectFactory(IObjectActivator activator, IModulePermissionObjectFactory permissionObjectFactory) : base(activator)
        {
            _permissionObjectFactory = permissionObjectFactory;
        }

        public IModulePermissionsList Create()
        {
            var list = CreateInstance<ModulePermissionsList>();
            MarkNew(list);
            MarkAsChild(list);
            return list;
        }

        public IModulePermissionsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ModulePermissionsList>();
            list.RaiseListChangedEvents = false;

            if (xpathNavigator != null)
            {
                foreach (XPathNavigator itemNav in xpathNavigator.Select("permission"))
                {
                    LoadFileItem(itemNav, list);
                }
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IModulePermissionsList list)
        {
            var item = _permissionObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}