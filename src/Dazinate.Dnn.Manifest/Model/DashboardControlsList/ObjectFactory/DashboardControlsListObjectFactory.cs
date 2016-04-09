using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Component.SubObjectFactory;
using Dazinate.Dnn.Manifest.Model.DashboardControl.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.DashboardControlsList.ObjectFactory
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