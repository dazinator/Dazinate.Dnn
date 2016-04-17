using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModulePermissionObjectFactory : BaseObjectFactory, IModulePermissionObjectFactory
    {

        public ModulePermissionObjectFactory(IObjectActivator activator) : base(activator)
        {
            //_packagesListFactory = packagesListFactory;
        }

        public IModulePermission Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<ModulePermission>();

            var code = XmlUtils.ReadAttribute(nav, "code");
            LoadProperty(businessObject, ModulePermission.CodeProperty, code);

            var key = XmlUtils.ReadAttribute(nav, "key");
            LoadProperty(businessObject, ModulePermission.KeyProperty, key);

            var name = XmlUtils.ReadAttribute(nav, "name");
            LoadProperty(businessObject, ModulePermission.NameProperty, name);

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}