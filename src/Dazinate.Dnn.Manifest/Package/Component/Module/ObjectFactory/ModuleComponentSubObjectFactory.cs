using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.Module.ObjectFactory
{
    public class ModuleComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {

        private readonly IEventMessageObjectFactory _eventMessageObjectFactory;
        private readonly ISupportedFeaturesListObjectFactory _supporedFeaturesListFactory;
        private readonly IModuleDefinitionsListObjectFactory _moduleDefinitionListFactory;


        public ModuleComponentSubObjectFactory(IObjectActivator activator,
            IEventMessageObjectFactory eventMessageObjectFactory,
            ISupportedFeaturesListObjectFactory supporedFeaturesListFactory,
            IModuleDefinitionsListObjectFactory moduleDefinitionListFactory
            ) : base(activator)
        {
            _eventMessageObjectFactory = eventMessageObjectFactory;
            _supporedFeaturesListFactory = supporedFeaturesListFactory;
            _moduleDefinitionListFactory = moduleDefinitionListFactory;
        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.Module;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<ModuleComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<ModuleComponent>();


            // todo: DesktopModule.
            var desktopModuleNode = nav.SelectSingleNode("desktopModule");
            if (desktopModuleNode == null)
            {
                throw new InvalidManifestFormatException();
            }

            var moduleName = XmlUtils.ReadElement(desktopModuleNode, "moduleName");
            LoadProperty(component, ModuleComponent.ModuleNameProperty, moduleName);

            var foldername = XmlUtils.ReadElement(desktopModuleNode, "foldername");
            LoadProperty(component, ModuleComponent.FolderNameProperty, foldername);

            var businessControllerClass = XmlUtils.ReadElement(desktopModuleNode, "businessControllerClass");
            LoadProperty(component, ModuleComponent.BusinessControllerClassProperty, businessControllerClass);

            var codeSubdirectory = XmlUtils.ReadElement(desktopModuleNode, "codeSubdirectory");
            LoadProperty(component, ModuleComponent.CodeSubDirectoryProperty, codeSubdirectory);

            var isAdminString = XmlUtils.ReadElement(desktopModuleNode, "isAdmin");
            bool? isAdminNullable = null;
            if (!string.IsNullOrWhiteSpace(isAdminString))
            {
                bool isAdmin;
                if (!bool.TryParse(isAdminString, out isAdmin))
                {
                    throw new InvalidManifestFormatException();
                }
                isAdminNullable = isAdmin;
            }
            LoadProperty(component, ModuleComponent.IsAdminProperty, isAdminNullable);


            var isPremiumString = XmlUtils.ReadElement(desktopModuleNode, "isPremium");
            bool? isPremiumNullable = null;
            if (!string.IsNullOrWhiteSpace(isPremiumString))
            {
                bool isPremium;
                if (!bool.TryParse(isPremiumString, out isPremium))
                {
                    throw new InvalidManifestFormatException();
                }
                isPremiumNullable = isPremium;
            }
            LoadProperty(component, ModuleComponent.IsPremiumProperty, isPremiumNullable);

            var supportedFeaturesNode = desktopModuleNode.SelectSingleNode("supportedFeatures");
           //   supportedFeaturesNode may be null as its not mandatory.
             var supportedFeaturesList = _supporedFeaturesListFactory.Fetch(supportedFeaturesNode);
            LoadProperty(component, ModuleComponent.SupportedFeaturesProperty, supportedFeaturesList);

            var moduleDefinitionsNode = desktopModuleNode.SelectSingleNode("moduleDefinitions"); 
            var moduleDefinitionsList = _moduleDefinitionListFactory.Fetch(moduleDefinitionsNode);
            LoadProperty(component, ModuleComponent.ModuleDefinitionsProperty, moduleDefinitionsList);

            var eventMessageNode = nav.SelectSingleNode("eventMessage");
            if (eventMessageNode != null)
            {
                var eventMessage = _eventMessageObjectFactory.Fetch(eventMessageNode);
                LoadProperty(component, ModuleComponent.EventMessageProperty, eventMessage);
            }

            return component;

        }


    }
}