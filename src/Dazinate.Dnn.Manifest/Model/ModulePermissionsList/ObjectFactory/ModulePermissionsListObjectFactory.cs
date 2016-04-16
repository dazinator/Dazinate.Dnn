using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.ModulePermission.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.ModulePermissionsList.ObjectFactory
{
    public class ModulePermissionsListObjectFactory : BaseObjectFactory, IModulePermissionsListObjectFactory
    {
        private readonly IModulePermissionObjectFactory _permissionObjectFactory;

        public ModulePermissionsListObjectFactory(IObjectActivator activator, IModulePermissionObjectFactory permissionObjectFactory) : base(activator)
        {
            _permissionObjectFactory = permissionObjectFactory;
        }

        public IModulePermissionsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<ModulePermissionsList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator itemNav in xpathNavigator.Select("permission"))
            {
                LoadFileItem(itemNav, list);
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