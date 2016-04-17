using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModuleDefinitionObjectFactory : BaseObjectFactory, IModuleDefinitionObjectFactory
    {

        private readonly IModuleControlsListObjectFactory _moduleControlsFactory;
        private readonly IModulePermissionsListObjectFactory _permissionsListObjectFactory;

        public ModuleDefinitionObjectFactory(IObjectActivator activator, IModuleControlsListObjectFactory moduleControlsFactory, IModulePermissionsListObjectFactory permissionsListObjectFactory) : base(activator)
        {
            _moduleControlsFactory = moduleControlsFactory;
            _permissionsListObjectFactory = permissionsListObjectFactory;
        }

        public IModuleDefinition Fetch(XPathNavigator nav)
        {
            // Create the correct concrete dependency based on the xml.
            var businessObject = CreateInstance<ModuleDefinition>();
            var friendlyName = XmlUtils.ReadElement(nav, "friendlyName");
            LoadProperty(businessObject, ModuleDefinition.FriendlyNameProperty, friendlyName);
            var defaultCacheTime = XmlUtils.ReadElement(nav, "defaultCacheTime");
            int cacheTime;
            if (!int.TryParse(defaultCacheTime, out cacheTime))
            {
                throw new InvalidManifestFormatException();
            }

            LoadProperty(businessObject, ModuleDefinition.DefaultCacheTimeProperty, cacheTime);

            var moduleControlsNode = nav.SelectSingleNode("moduleControls");
            var moduleControlsList = _moduleControlsFactory.Fetch(moduleControlsNode);
            LoadProperty(businessObject, ModuleDefinition.ModuleControlsListProperty, moduleControlsList);

            var permissionsNode = nav.SelectSingleNode("permissions");
            var permissionsList = _permissionsListObjectFactory.Fetch(permissionsNode);
            LoadProperty(businessObject, ModuleDefinition.ModulePermissionsListProperty, permissionsList);
          

            MarkAsChild(businessObject);
            MarkOld(businessObject);
            CheckRules(businessObject);
            return businessObject;
        }
    }
}