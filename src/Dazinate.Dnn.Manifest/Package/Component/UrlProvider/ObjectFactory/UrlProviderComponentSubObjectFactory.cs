using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Exceptions;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Package.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Utils;

namespace Dazinate.Dnn.Manifest.Package.Component.UrlProvider.ObjectFactory
{
    public class UrlProviderComponentSubObjectFactory : BaseObjectFactory, IComponentSubObjectFactory
    {
        public UrlProviderComponentSubObjectFactory(IObjectActivator activator) : base(activator)
        {

        }

        public ComponentType ComponentType
        {
            get
            {
                return ComponentType.UrlProvider;
            }
        }


        public IComponent Create(ComponentType componentType)
        {
            var component = CreateInstance<UrlProviderComponent>();
            MarkAsChild(component);
            MarkNew(component);
            return component;
        }

        public IComponent Fetch(XPathNavigator nav)
        {
            var component = CreateInstance<UrlProviderComponent>();
            var node = nav.SelectSingleNode("urlProvider");
            if (node == null)
            {
                throw new InvalidManifestFormatException();
            }

            var name = XmlUtils.ReadElement(node, "name");
            LoadProperty(component, UrlProviderComponent.NameProperty, name);

            var type = XmlUtils.ReadElement(node, "type");
            LoadProperty(component, UrlProviderComponent.TypeProperty, type);

            var settingsControlSrc = XmlUtils.ReadElement(node, "settingsControlSrc");
            LoadProperty(component, UrlProviderComponent.SettingsControlSourceProperty, settingsControlSrc);

            var redirectAllUrls = XmlUtils.ReadElement(node, "redirectAllUrls");
            var boolValue = bool.Parse(redirectAllUrls);
            LoadProperty(component, UrlProviderComponent.RedirectAllUrlsProperty, boolValue);

            var replaceAllUrls = XmlUtils.ReadElement(node, "replaceAllUrls");
            boolValue = bool.Parse(replaceAllUrls);
            LoadProperty(component, UrlProviderComponent.ReplaceAllUrlsProperty, boolValue);

            var rewriteAllUrls = XmlUtils.ReadElement(node, "rewriteAllUrls");
            boolValue = bool.Parse(rewriteAllUrls);
            LoadProperty(component, UrlProviderComponent.RewriteAllUrlsProperty, boolValue);
            
            var desktopModule = XmlUtils.ReadElement(node, "desktopModule");
            LoadProperty(component, UrlProviderComponent.DesktopModuleProperty, desktopModule);

            return component;
        }


    }
}