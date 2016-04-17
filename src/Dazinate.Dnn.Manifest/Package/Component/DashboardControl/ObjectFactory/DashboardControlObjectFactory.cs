using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.DashboardControl.ObjectFactory
{
    public class DashboardControlObjectFactory : BaseObjectFactory, IDashboardControlObjectFactory
    {

        public DashboardControlObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IDashboardControl Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<DashboardControl>();

            var key = XmlUtils.ReadElement(nav, "key");
            LoadProperty(businessObject, DashboardControl.KeyProperty, key);

            var src = XmlUtils.ReadElement(nav, "src");
            LoadProperty(businessObject, DashboardControl.SourceProperty, src);

            var localResource = XmlUtils.ReadElement(nav, "localResources");
            LoadProperty(businessObject, DashboardControl.LocalResourcesFileProperty, localResource);

            var controllerClass = XmlUtils.ReadElement(nav, "controllerClass");
            LoadProperty(businessObject, DashboardControl.ControllerClassProperty, controllerClass);

            var isEnabled = XmlUtils.ReadElement(nav, "isEnabled");
            bool isEnabledBool = bool.Parse(isEnabled);
            LoadProperty(businessObject, DashboardControl.IsEnabledProperty, isEnabledBool);
            
            var viewOrder = XmlUtils.ReadElement(nav, "viewOrder");
            int viewOrderInt = int.Parse(viewOrder);
            LoadProperty(businessObject, DashboardControl.ViewOrderProperty, viewOrderInt);


            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}