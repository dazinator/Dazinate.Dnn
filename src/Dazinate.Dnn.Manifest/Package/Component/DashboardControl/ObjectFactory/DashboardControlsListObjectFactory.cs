using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory
{
    public class DashboardControlsListObjectFactory : BaseObjectFactory, IDashboardControlsListObjectFactory
    {
        private readonly IDashboardControlObjectFactory _fileObjectFactory;

        public DashboardControlsListObjectFactory(IObjectActivator activator, IDashboardControlObjectFactory fileObjectFactory) : base(activator)
        {
            _fileObjectFactory = fileObjectFactory;
        }

        public IDashboardControlsList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<DashboardControlsList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator itemNav in xpathNavigator.Select("dashboardControl"))
            {
                LoadFileItem(itemNav, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadFileItem(XPathNavigator nav, IDashboardControlsList list)
        {
            var item = _fileObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}