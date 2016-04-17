using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModuleControlObjectFactory : BaseObjectFactory, IModuleControlObjectFactory
    {

        public ModuleControlObjectFactory(IObjectActivator activator) : base(activator)
        {

        }

        public IModuleControl Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<ModuleControl>();

            var key = XmlUtils.ReadElement(nav, "controlKey");
            LoadProperty(businessObject, ModuleControl.ControlKeyProperty, key);

            var source = XmlUtils.ReadElement(nav, "controlSrc");
            LoadProperty(businessObject, ModuleControl.ControlSourceProperty, source);


            bool? supportsPartialRenderingNullable = null;

            var supportsPartialRenderingString = XmlUtils.ReadElement(nav, "supportsPartialRendering");
            if (!string.IsNullOrWhiteSpace(supportsPartialRenderingString))
            {
                bool supportsPartialRendering;
                if (!bool.TryParse(supportsPartialRenderingString, out supportsPartialRendering))
                {
                    throw new InvalidManifestFormatException();
                }

                supportsPartialRenderingNullable = supportsPartialRendering;
            }

            LoadProperty(businessObject, ModuleControl.SupportsPartialRenderingProperty, supportsPartialRenderingNullable);


            var controlTitle = XmlUtils.ReadElement(nav, "controlTitle");
            LoadProperty(businessObject, ModuleControl.ControlTitleProperty, controlTitle);

            var controlType = XmlUtils.ReadElement(nav, "controlType");
            LoadProperty(businessObject, ModuleControl.ControlTypeProperty, controlType);

            var iconFile = XmlUtils.ReadElement(nav, "iconFile");
            LoadProperty(businessObject, ModuleControl.IconFileProperty, iconFile);

            var helpUrl = XmlUtils.ReadElement(nav, "helpUrl");
            LoadProperty(businessObject, ModuleControl.HelpUrlProperty, helpUrl);

            int? viewOrderNullable = null;
            var viewOrderString = XmlUtils.ReadElement(nav, "viewOrder");
            if (!string.IsNullOrWhiteSpace(viewOrderString))
            {
                int viewOrder;
                if (!int.TryParse(viewOrderString, out viewOrder))
                {
                    throw new InvalidManifestFormatException();
                }
                viewOrderNullable = viewOrder;
            }
           
            LoadProperty(businessObject, ModuleControl.ViewOrderProperty, viewOrderNullable);
            

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}